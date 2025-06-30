using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using xwx.TDP.Library.Common.Utils;

namespace xwx.TDP.Library.Common
{
    internal class Configurations : CommonConfig
    {
        #region Singlton
        private readonly static object PadLock = new object();
        private static Configurations _instance = null;
        private Configurations()
        {
        }
        public static Configurations Instance
        {
            get
            {
                lock (PadLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Configurations();
                    }
                    return _instance;
                }
            }
        }
        #endregion Singlton
        #region Properties
        #region 数据库驱动配置
        //private string _dbDriverType = "RealDbDriver";
        //[Category("A. 数据库驱动"),
        //DisplayName("A01.驱动类型"),
        //Description("驱动类型：Simulate,RealDbDriver,..."),
        //DefaultValue(typeof(string), "RealDbDriver"),
        //Editor(typeof(EditorDropDown<DatabaseFactory>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public string DbDriverType
        //{
        //    get { return _dbDriverType; }
        //    set { _dbDriverType = value; }
        //}
        //private bool _passCheckTimeNeedUpdated = false;
        //[Category("A. 数据库驱动"),
        //DisplayName("A02.Pass Check 时间自动更新"),
        //DefaultValue(typeof(string), "false"),
        //]
        //public bool PassCheckTimeNeedUpdated
        //{
        //    get { return _passCheckTimeNeedUpdated; }
        //    set { _passCheckTimeNeedUpdated = value; }
        //}
        //private DateTime _passCheckTime = new DateTime(2010, 1, 1);
        //[Category("A. 数据库驱动"),
        //DisplayName("A03.Pass Check 检测时间"),
        //DefaultValue(typeof(string), "2010-1-1"),
        //]
        //public DateTime PassCheckTime
        //{
        //    get { return _passCheckTime; }
        //    set { _passCheckTime = value; }
        //}

        //private string _defaultTester = "99999";
        //[Category("A. 数据库驱动"),
        //DisplayName("A04.测试人员工号"),
        //DefaultValue(typeof(string), "99999"),
        //]
        //public string DefaultTester
        //{
        //    get { return _defaultTester; }
        //    set { _defaultTester = value; }
        //}
        //private string _defaultProductName = "RRU";
        //[Category("A. 数据库驱动"),
        //DisplayName("A05.测试产品类型"),
        //DefaultValue(typeof(string), "RRU"),
        //]
        //public string DefaultProductName
        //{
        //    get { return _defaultProductName; }
        //    set { _defaultProductName = value; }
        //}
        //private string _defaultTestPhase = "系统测试";
        //[Category("A. 数据库驱动"),
        //DisplayName("A06.测试阶段"),
        //DefaultValue(typeof(string), "系统测试"),
        //]
        //public string DefaultTestPhase
        //{
        //    get { return _defaultTestPhase; }
        //    set { _defaultTestPhase = value; }
        //}

        //private TestModeEnum _testMode = TestModeEnum.Normal;
        //[Category("A. 数据库驱动"),
        //DisplayName("A07.测试模式"),
        //DefaultValue(typeof(TestModeEnum), "Normal"),
        //]
        //public TestModeEnum TestMode
        //{
        //    get { return _testMode; }
        //    set { _testMode = value; }
        //}

        //private string _testBench = "ST001";
        //[Category("A. 数据库驱动"),
        //DisplayName("A08.测试工位"),
        //DefaultValue(typeof(string), "ST001"),
        //]
        //public string DefaultTestBench
        //{
        //    get { return _testBench; }
        //    set { _testBench = value; }
        //}

        #endregion      
        #region 线损文件配置
        private string _defaultPathLossFile = "";
        [Category("B. 线损文件配置"),
        DisplayName("B01.线损文件"),
        Description("线损文件配置，不配置情况下读取系统默认文件。"),
        DefaultValue(typeof(string), ""),
        Editor(typeof(EditorFileSilect<PgfFile>), typeof(UITypeEditor)),
        ]
        public string DefaultPathLossFile
        {
            get { return _defaultPathLossFile; }
            set { _defaultPathLossFile = value; }
        }
        #endregion 线损文件配置LoadDefaultConfigFile
        #region 线损文件配置
        private string _defaultSwitchConfigureFile = "";
        [Category("B. 开关文件配置"),
        DisplayName("B02.开关文件配置"),
        Description("开关文件配置，不配置情况下读取系统默认文件。"),
        DefaultValue(typeof(string), ""),
        Editor(typeof(EditorFileSilect<XmlFile>), typeof(UITypeEditor)),
        ]
        public string DefaultSwitchConfigureFile
        {
            get { return _defaultSwitchConfigureFile; }
            set { _defaultSwitchConfigureFile = value; }
        }
        #endregion 线损文件配置

        #region 日志信息

        //private bool _showInstrumentsLog = true;
        //[Category("C. 日志信息"),
        //DisplayName("C01.仪表LOG显示"),
        //DefaultValue(typeof(bool), "true"),
        //]
        //public bool ShowInstrumentsLog
        //{
        //    get { return _showInstrumentsLog; }
        //    set { _showInstrumentsLog = value; }
        //}
        #endregion 日志信息

        #region 默认脚本编辑器
        //private string _defaultEditor = "notepad.exe";
        //[Category("D. 脚本编辑器"),
        //DisplayName("C01.用于C#的脚本编辑功能"),
        //DefaultValue(typeof(string), "notepad.exe"),
        //Editor(typeof(FileSelector), typeof(UITypeEditor))
        //]
        //public string DefaultEditor
        //{
        //    get { return _defaultEditor; }
        //    set { _defaultEditor = value; }
        //}
        #endregion 默认脚本编辑器

        #region Tcb Driver configurations
        //private string _tcbDriver = "SimulateTcb";
        //[Category("D. Tcb驱动配置"),
        //DisplayName("D01.Tcb类型"),
        //DefaultValue(typeof(string), "SimulateTcb"),
        //Editor(typeof(EditorDropDown<TcbFactory>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public string TcbDriver
        //{
        //    get { return _tcbDriver; }
        //    set { _tcbDriver = value; }
        //}

        //private string _comPortName = "COM1";
        //[Category("D. Tcb驱动配置"),
        //DisplayName("D02.Tcb使用的端口"),
        //DefaultValue(typeof(string), "COM1"),
        //]
        //public string ComPortName
        //{
        //    get { return _comPortName; }
        //    set { _comPortName = value; }
        //}

        //private int _baudRate = 4800;
        //[Category("D. Tcb驱动配置"),
        //DisplayName("D03.Tcb使用的波特率"),
        //DefaultValue(typeof(int), "4800"),
        //]
        //public int BaudRate
        //{
        //    get { return _baudRate; }
        //    set { _baudRate = value; }
        //}

        #endregion TcbDriver configurations


        #region PM配置
        //private float _pmWaitTimeOutS = 240;
        //[Category("E. PM配置"),
        //DisplayName("E01.PM等待时间"),
        //DefaultValue(typeof(float), "0")
        //]
        //public float PmWaitTimeOutS
        //{
        //    get { return _pmWaitTimeOutS; }
        //    set { _pmWaitTimeOutS = value; }
        //}


        //private float _pmTestTimeOutS = 35;
        //[Category("E. PM配置"),
        //DisplayName("E02.PM检测等待时间"),
        //DefaultValue(typeof(float), "0")
        //]
        //public float PmTestTimeOutS
        //{
        //    get { return _pmTestTimeOutS; }
        //    set { _pmTestTimeOutS = value; }
        //}
        #endregion PM配置

        #endregion Properties

        private class FileSelector : UITypeEditor
        {
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                string file = value as string;
                OpenFileDialog fd = new OpenFileDialog();
                fd.CheckFileExists = true;
                fd.DefaultExt = "exe";
                fd.FileName = file;
                fd.Filter = "(*.exe)|*.exe|(*.*)|*.*";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    value = fd.FileName;
                }
                return base.EditValue(context, provider, value);
            }
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
        }
    }
    class EditorDropDown<T> : UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            DriverTypeSelector sis = new DriverTypeSelector(edSvc);
            sis.SelectedItem = value.ToString();
            edSvc.DropDownControl(sis);
            return sis.SelectedItem;
        }
        class DriverTypeSelector : UserControl
        {
            private string _selectedItem;
            public string SelectedItem
            {
                get { return this._selectedItem; }
                set { _selectedItem = value; }
            }

            public DriverTypeSelector(IWindowsFormsEditorService eds)
            {
                object[] drvs = { new List<string>() };
                //DatabaseFactory.GetSupportedDrivers(out drvs);
                MethodInfo minfo;
                minfo = typeof(T).GetMethod("GetSupportedDrivers");
                if (minfo != null)
                {
                    minfo.Invoke(null, drvs);
                }

                ListBox _listBox = new ListBox();
                _listBox.Items.AddRange((drvs[0] as List<string>).ToArray());
                _listBox.Dock = DockStyle.Fill;
                _listBox.IntegralHeight = false;
                _listBox.BorderStyle = BorderStyle.None;
                _listBox.SelectedIndexChanged += delegate (object sender, EventArgs e)
                {
                    if ((sender as ListControl).SelectedIndex >= 0)
                    {
                        this._selectedItem = (sender as ListControl).Text;
                    }
                    eds.CloseDropDown();
                };
                this.Controls.Add(_listBox);
            }
        }
    }
    class EditorFileSilect<T> : UITypeEditor where T : IFileExt, new()
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            T info = new T();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            FileInfo fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            openFileDialog1.InitialDirectory = fileInfo.DirectoryName + "\\config";
            openFileDialog1.Filter = string.Format("{0} files (*.{0})|*.{0}|All files (*.*)|*.*", info.FileExName);
            //openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileInfo = new FileInfo(openFileDialog1.FileName);
                return fileInfo.Name;
            }

            return info.DefaultFile;
        }
    }

    internal interface IFileExt
    {
        string FileExName { get; }
        string DefaultFile { get; }
    }
    class XmlFile : IFileExt
    {

        #region IFileExt Members

        public string FileExName
        {
            get { return "xml"; }
        }
        public string DefaultFile
        {
            get { return Configurations.Instance.DefaultSwitchConfigureFile; }
        }

        #endregion
    }
    class PgfFile : IFileExt
    {
        public string FileExName { get { return "pgf"; } }
        public string DefaultFile
        {
            get { return Configurations.Instance.DefaultPathLossFile; }
        }
    }
    enum TestModeEnum
    {
        Normal,
        Maintenance,
    }
}
