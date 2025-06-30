using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using xwx.TDP.Library.BaseCase.Logger;

namespace xwx.TDP.Library.BaseCase
{
    public class AppHooker : IAppHooker
    {
        public class TestResultItemName
        {
            public const string Temperature = "Temperature";

            public const string Vlotage = "Voltage";
        }

        private Form _onwerForm;

        public Form OnwerForm
        {
            set
            {
                _onwerForm = value;
            }
        }

        public bool IsDebugMode
        {
            get
            {
                return true;
            }
        }

        public bool IsTestManagerApp
        {
            get
            {
                return true;
            }
        }

        public uint MaxExecutorNum
        {
            get
            {
                return 1u;
            }
        }

        public ToolStripItem[] ToolStripItems
        {
            get
            {
                List<ToolStripItem> list = new List<ToolStripItem>();
                //ToolStripButton toolStripButton = new ToolStripButton("Super Lite");
                //toolStripButton.Tag = "SuperLite|Lite|Standard|Premium";
                //list.Add(toolStripButton);
                //ToolStripButton toolStripButton2 = new ToolStripButton("Lite");
                //toolStripButton2.Tag = "Lite|Standard|Premium";
                //list.Add(toolStripButton2);
                //ToolStripButton toolStripButton3 = new ToolStripButton("Standard");
                //toolStripButton3.Tag = "Standard|Premium";
                //list.Add(toolStripButton3);
                //ToolStripButton toolStripButton4 = new ToolStripButton("Premium");
                //toolStripButton4.Tag = "Premium";
                //list.Add(toolStripButton4);
                return list.ToArray();
            }
        }

        public string[] TestResultTitles
        {
            get
            {
                return new string[2] { "Temperature", "Voltage" };
            }
        }

        public Type[] UserDefinedLogViewsType
        {
            get
            {
                Type[] exportedTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
                List<Type> list = new List<Type>();
                Type[] array = exportedTypes;
                foreach (Type type in array)
                {
                    if (type.GetInterface(typeof(INLogger).FullName, false) != null)
                    {
                        list.Add(type);
                    }
                }
                //list.Add(typeof(TableLogView));
                //list.Add(typeof(TreeLogView));
                return list.ToArray();
            }
        }

        public string DefaultTestResultDirectory
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }
    }
}
