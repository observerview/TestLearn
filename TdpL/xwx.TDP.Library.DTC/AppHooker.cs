using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern.Interface;
using TestManager.Extern;
using System.Windows.Forms;

namespace xwx.TDP.Library.DTC
{
    public class AppHooker : IAppHooker
    {
        private Form _onwerForm = null;
        public Form OnwerForm
        {
            set { _onwerForm = value; }
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
        public ToolStripItem[] ToolStripItems
        {
            get
            {
                List<ToolStripItem> toolStripItems = new List<ToolStripItem>();

                //ToolStripButton funcFormOpen = new ToolStripButton("设置", Properties.Resources.modify);
                //funcFormOpen.ToolTipText = "Show Setting";
                //funcFormOpen.Click += new EventHandler(funcFormOpen_Click);
                //toolStripItems.Add(funcFormOpen);
                //can create a button to open a page

                return toolStripItems.ToArray();
            }
        }
        void funcFormOpen_Click(object sender, EventArgs e)
        {
            //new funcForm().ShowDialog(_onwerForm);
            //创建一个funcForm，用上面方法，就能在点击的时候打开了
        }

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
    }
}
