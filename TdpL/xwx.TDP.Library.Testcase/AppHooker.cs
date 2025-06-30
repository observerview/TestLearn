using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using TestManager.Extern.Interface;
using xwx.TDP.Library.Common.Forms;
using xwx.TDP.Library.Common.Utils;

namespace xwx.TDP.Library.Common
{
    public class AppHooker : IAppHooker
    {
        #region Member
        private Form _onwerForm = null;
        #endregion Member

        #region IAppHooker Members
        #region Misc
        public Form OnwerForm
        {
            set { this._onwerForm = value; }
        }

        public bool IsDebugMode
        {
            get { return true; }
        }

        public bool IsTestManagerApp
        {
            get { return true; }
        }

        public uint MaxExecutorNum
        {
            get { return 1; }
        }
        #endregion Misc

        #region ToolbarButton Related
        public ToolStripItem[] ToolStripItems
        {
            get
            {
                List<ToolStripItem> toolStripItems = new List<ToolStripItem>();

                ToolStripButton tsbConfig = new ToolStripButton("Setting",Properties.Resources.modify);
                tsbConfig.ToolTipText = "Show Setting";
                tsbConfig.Click += new EventHandler(tsbConfig_Click);
                toolStripItems.Add(tsbConfig);

                //ToolStripButton report = new ToolStripButton("Report", Properties.Resource.print);
                //report.ToolTipText = "Reports editor";
                //report.Click += new EventHandler(report_Click);
                //toolStripItems.Add(report);

                //ToolStripButton tool = new ToolStripButton("Database Connection Setting", Properties.Resource.database);
                //tool.ToolTipText = "Reports editor";
                //tool.Click += new EventHandler(tool_Click);
                //toolStripItems.Add(tool);

                return toolStripItems.ToArray();
            }
        }

        //void tool_Click(object sender, EventArgs e)
        //{
        //    ConnectionBuilder.Instance.ShowDialog(this._onwerForm);

        //    IDatabase db = DatabaseFactory.CreateInstance(Configurations.Instance.DbDriverType);
        //    db.Connection = new System.Data.OleDb.OleDbConnection(Utils.ConnectionBuilder.Instance.Conn);
        //    db.Logon();
        //    if (db.Initialize() == false)
        //    {
        //        MessageBox.Show(db.ErrMsg);
        //        return;
        //    }
        //    db.Logoff();
        //}

        //void report_Click(object sender, EventArgs e)
        //{
        //    Report report = new Report();
        //    //report.Load("c:\\a.frx");
        //    report.Show();
        //}
        #endregion ToolbarButton Related

        #region Test Result Items Name
        public class TestResultItemName
        {
            public const string Temperature = "Temperature";
            public const string Vlotage = "Voltage";
        }

        public string[] TestResultTitles
        {
            get
            {
                return new string[]{    TestResultItemName.Temperature,
                                        TestResultItemName.Vlotage,
                                    };
            }
        }
        #endregion Test Result Items Name

        #region LogView
        public Type[] UserDefinedLogViewsType
        {
            get
            {
                Type[] exportedTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
                List<Type> logViewTypes_NLogger = new List<Type>();
                foreach (Type type in exportedTypes)
                {
                    if (type.GetInterface(typeof(ILogViewEx).FullName, false) != null)
                    {
                        logViewTypes_NLogger.Add(type);
                    }
                }
                return logViewTypes_NLogger.ToArray();
            }
        }
        #endregion LogView
        #region ResultDirectory
        public string DefaultTestResultDirectory
        {
            get { return string.Empty; }
            set { }
        }
        #endregion ResultDirectory

        void tsbConfig_Click(object sender, EventArgs e)
        {
            new SystemConfig().ShowDialog(_onwerForm);
        }
        #endregion
    }
}
