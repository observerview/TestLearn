using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xwx.TDP.Library.Common.Utils
{
    public abstract class CommonConfig : CommonConfigBase
    {
        public CommonConfig()
            : base()
        {
            string configFileName = System.Reflection.Assembly.GetCallingAssembly().Location + ".tdplconfig";
            if (File.Exists(configFileName))
            {
                this.LoadFromFile(configFileName);
            }
        }

        public bool Save()
        {
            string configFileName = System.Reflection.Assembly.GetCallingAssembly().Location + ".tdplconfig";
            return this.SaveToFile(configFileName);
        }
    }

    // 存放环境相关的配置，不同的部署 PC 机上，值不一样。如仪表类型、资源地址等
    public abstract class CommonSetting : CommonConfigBase
    {
        public CommonSetting()
            : base()
        {
            string configFileName = System.Reflection.Assembly.GetCallingAssembly().Location + ".tdplsetting";
            if (File.Exists(configFileName))
            {
                this.LoadFromFile(configFileName);
            }
        }

        public bool Save()
        {
            string configFileName = System.Reflection.Assembly.GetCallingAssembly().Location + ".tdplsetting";
            return this.SaveToFile(configFileName);
        }
    }

    public abstract class CommonConfigBase
    {
        DataTable _table;
        public CommonConfigBase()
        {
            _table = new DataTable("Config");
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("Value", typeof(object));
            //_table.PrimaryKey = new DataColumn[] { _table.Columns[0] };
        }
        public bool LoadFromFile(string configFileName)
        {
            _table.Rows.Clear();
            try
            {
                _table.ReadXml(configFileName);
                foreach (DataRow row in _table.Rows)
                {
                    PropertyInfo pi = this.GetType().GetProperty(row["Name"].ToString());
                    if (pi == null || pi.PropertyType.IsSubclassOf(typeof(CommonConfigBase)))
                    {
                        continue;
                    }
                    if (!pi.CanWrite) continue;
                    if (pi.PropertyType.IsArray)
                    {
                        DataTable arr = new DataTable("Array");
                        arr.Columns.Add("Index", typeof(int));
                        arr.Columns.Add("Value", typeof(object));
                        StringReader reader = new StringReader(row["Value"].ToString());
                        arr.ReadXml(reader);
                        Array vals = Array.CreateInstance(pi.PropertyType.GetElementType(), arr.Rows.Count);
                        for (int i = 0; i < arr.Rows.Count; i++)
                        {
                            vals.SetValue(arr.Rows[i]["Value"], i);
                        }
                        pi.SetValue(this, vals, null);
                    }
                    else
                    {
                        pi.SetValue(this, row["Value"], null);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
            return true;
        }
        public bool SaveToFile(string configFileName)
        {
            _table.Rows.Clear();
            try
            {
                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    if (pi.PropertyType.IsSubclassOf(typeof(CommonConfigBase)))
                        continue;
                    DataRow row = _table.NewRow();
                    row["Name"] = pi.Name;
                    if (!pi.PropertyType.IsSerializable) continue;
                    if (!pi.CanRead) continue;
                    if (pi.PropertyType.IsArray)
                    {
                        DataTable arr = new DataTable("Array");
                        arr.Columns.Add("Index", typeof(int));
                        arr.Columns.Add("Value", typeof(object));
                        Array vals = pi.GetValue(this, null) as Array;
                        if (vals == null) continue;
                        for (int i = 0; i < vals.Length; i++)
                        {
                            DataRow nr = arr.NewRow();
                            nr["Index"] = i;
                            nr["Value"] = vals.GetValue(i);
                            arr.Rows.Add(nr);
                        }
                        StringWriter writer = new StringWriter();
                        arr.WriteXml(writer);
                        row["Value"] = writer.ToString();
                    }
                    else if (pi.PropertyType.IsEnum)
                    {
                        row["Value"] = (int)pi.GetValue(this, null);
                    }
                    else
                    {
                        row["Value"] = pi.GetValue(this, null);
                    }
                    _table.Rows.Add(row);
                }
                _table.WriteXml(configFileName, true);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
            return true;
        }
    }
}
