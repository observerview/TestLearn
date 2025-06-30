using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using xwx.TDP.Library.Common.Utils;
using xwx.TDP.Common.Instruments;
using static xwx.TDP.Common.Instruments.MessageInstruments;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using xwx.TDP.Common.Instruments.PSU;
using xwx.TDP.Library.Common.Forms;

namespace xwx.TDP.Library.Common
{
    internal class Settings : CommonSetting
    {
        #region Singlton
        //单例模式，防止外部创建多实例出现冲突问题。
        //用lock保证线程安全，貌似也可以用静态构造器？还有一个lazy关键字好像也可以。没空研究，就这样
        private readonly static object PadLock = new object();
        private static Settings _instance = null;
        private Settings()
        {
        }
        public static Settings Instance
        {
            get
            {
                lock (PadLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Settings();
                    }
                    return _instance;
                }
            }
        }
        #endregion Singlton

        #region Properties

        #region 仪表驱动配置
        //private DevInfo _saDriver = new DevInfo("Simulated SA");
        //[Category("B. 仪表驱动"),
        //DisplayName("B01.频谱仪类型"),
        //Description("驱动类型：Simulated SA,MXA/EXA,..."),
        //Editor(typeof(InstrumetsTypeEditorDropDown<SpectrumAnalyzer>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo SaDriver
        //{
        //    get { return _saDriver; }
        //    set { _saDriver = value; }
        //}

        //private DevInfo _sgDriver = new DevInfo("Simulated SG");
        //[Category("B. 仪表驱动"),
        //DisplayName("B02.信号源类型"),
        //Description("驱动类型：Simulated SG,..."),
        //Editor(typeof(InstrumetsTypeEditorDropDown<SignalGenerator>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo SgDriver
        //{
        //    get { return _sgDriver; }
        //    set { _sgDriver = value; }
        //}


        //private DevInfo[] _swsDriver = new DevInfo[] { new DevInfo("Simulated SW") };
        //[Category("B. 仪表驱动"),
        //DisplayName("B03.开关类型"),
        //Description("驱动类型：Simulated SW,..."),
        //]
        //public DevInfo[] SwsDriver
        //{
        //    get { return _swsDriver; }
        //    set { _swsDriver = value; }
        //}


        private DevInfo _psuDriver = new DevInfo("Simulated PSU");
        [Category("B. 仪表驱动"),
        DisplayName("B04.电源类型"),
        Description("驱动类型：Simulated PSU,..."),
        DefaultValue(typeof(string), "Simulated PSU"),
        Editor(typeof(InstrumetsTypeEditorDropDown<PowerSupplyUnit>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        ]
        public DevInfo PsuDriver
        {
            get { return _psuDriver; }
            set { _psuDriver = value; }
        }


        //private DevInfo _naDriver = new DevInfo("Simulated NA");
        //[Category("B. 仪表驱动"),
        //DisplayName("B06.矢网类型"),
        //Description("驱动类型：Simulated NA,..."),
        //DefaultValue(typeof(string), "Simulated NA"),
        //Editor(typeof(InstrumetsTypeEditorDropDown<NetworkAnalyzer>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo NaDriver
        //{
        //    get { return _naDriver; }
        //    set { _naDriver = value; }
        //}

        //private DevInfo _mmDriver = new DevInfo("Simulated MM");
        //[Category("B. 仪表驱动"),
        //DisplayName("B07.万用表类型"),
        //Description("驱动类型：Simulated MM,..."),
        //Editor(typeof(InstrumetsTypeEditorDropDown<MultiMeter>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo MmDriver
        //{
        //    get { return _mmDriver; }
        //    set { _mmDriver = value; }
        //}

        //private DevInfo _elDriver = new DevInfo("Simulated EL");
        //[Category("B. 仪表驱动"),
        //DisplayName("B08.电子负载"),
        //Description("驱动类型：Simulated EL,..."),
        //Editor(typeof(InstrumetsTypeEditorDropDown<ElectronicLoad>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo ElDriver
        //{
        //    get { return _elDriver; }
        //    set { _elDriver = value; }
        //}


        //private DevInfo _ctsDriver = new DevInfo("Simulate CTS");
        //[Category("B. 仪表驱动"),
        //DisplayName("B09.综测仪类型"),
        //Description("驱动类型：Simulate CTS,..."),
        //Editor(typeof(InstrumetsTypeEditorDropDown<CommunicationTestSet>), typeof(System.Drawing.Design.UITypeEditor)),   // Define the UI Editor of this property
        //]
        //public DevInfo CtsDriver
        //{
        //    get { return _ctsDriver; }
        //    set { _ctsDriver = value; }
        //}

        //private bool _autoPowerOff = true;
        //[Category("B. 仪表驱动"),
        //DisplayName("C01.自动下电"),
        //Description("如设置为true，在测试PASS后自动下电，fail后提示是否下电\n"
        //    + "false 则不提示和自动下电"),
        //DefaultValue(typeof(bool), "true"),
        //]
        //public bool AutoPowerOff
        //{
        //    get { return _autoPowerOff; }
        //    set { _autoPowerOff = value; }
        //}

        //private bool _autoPowerOffConfirm = true;
        //[Category("B. 仪表驱动"),
        //DisplayName("C02.自动下电提示"),
        //Description("如设置为true，fail后提示是否下电，否则fail后直接下电\n"),
        //DefaultValue(typeof(bool), "true"),
        //]
        //public bool AutoPowerOffConfirm
        //{
        //    get { return _autoPowerOffConfirm; }
        //    set { _autoPowerOffConfirm = value; }
        //}

        //todo
        private bool _isRemoteDriverType = false;
        [Category("D. 仪表驱动"),
        DisplayName("D01.是否使用远程仪表驱动"),
        Description("仪表驱动类型选择，远程可实现仪表共享"),
        DefaultValue(typeof(bool), "false"),
        ]
        public bool IsRemoteDriverType
        {
            get { return _isRemoteDriverType; }
            set { _isRemoteDriverType = value; }
        }

        //todo
        private string _instrumentsServerIp = "192.168.0.1";
        [Category("D. 仪表驱动"),
        DisplayName("D02.仪表驱动服务器地址"),
        Description("仪表驱动服务器地址,todo"),
        DefaultValue(typeof(string), "192.168.0.1"),
        ]
        public string InstrumentsServerIp
        {
            get { return _instrumentsServerIp; }
            set { _instrumentsServerIp = value; }
        }
        #endregion

        #region 数据库驱动配置
        //目前不需要
        #endregion 数据库驱动配置

        #endregion Properties
    }

    internal class InstrumetsTypeEditorDropDown<T> : UITypeEditor where T : MessageInstruments
    {
        IWindowsFormsEditorService edSvc = null;
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            DevInfo dev = (DevInfo)((DevInfo)value).Clone();
            DevSettings<T> dlg = new DevSettings<T>(dev);
            DialogResult ret = edSvc.ShowDialog(dlg);
            if (ret == DialogResult.OK)
            {
                return dev;
            }
            return value;
        }
    }

    //[Serializable,
    //Editor(typeof(InstrumetsTypeEditorDropDown<MultifunctionSwitch>), typeof(System.Drawing.Design.UITypeEditor)),
    //]
    [Serializable]
    public class DevInfo : ICloneable, IXmlSerializable
    {

        private string _devType = "Simulated SW";

        //private string _switchFile = "";
        //public string SwitchFile
        //{
        //    get { return _switchFile; }
        //    set { _switchFile = value; }
        //}

        public DevInfo()
        {

        }

        public DevInfo(string defaultTypeName)
        {
            _devType = defaultTypeName;
        }

        public string DevType
        {
            get { return _devType; }
            set { _devType = value; }
        }
        private string _devAddr = "TCPIP0::192.168.100.100::INSTR";

        public string DevAddr
        {
            get { return _devAddr; }
            set { _devAddr = value; }
        }
        private bool _global = true;

        public bool Global
        {
            get { return _global; }
            set { _global = value; }
        }
        private bool _isRemote = false;

        public bool IsRemote
        {
            get { return _isRemote; }
            set { _isRemote = value; }
        }
        private string _svrAddr = "";

        public string SvrAddr
        {
            get { return _svrAddr; }
            set { _svrAddr = value; }
        }

        public override string ToString()
        {
            bool remote = _global ? Settings.Instance.IsRemoteDriverType : _isRemote;
            string svr = _global ? Settings.Instance.InstrumentsServerIp : _svrAddr;
            string tmp = string.Format("Type:{0}; Addr:{1}; Global:{2}; Remote:{3}; Server:{4};",
                _devType, _devAddr, _global, remote, svr);
            //if (_switchFile != "")
            //{
            //    tmp = string.Format("Type:{0}; Addr:{1}; Global:{2}; Remote:{3}; Server:{4};File: {5}",
            //    _devType, _devAddr, _global, remote, svr, SwitchFile);
            //}
            return tmp;
        }

        public object Clone()
        {
            DevInfo dev = new DevInfo();
            dev.DevType = DevType;
            dev.DevAddr = DevAddr;
            dev.Global = Global;
            dev.IsRemote = IsRemote;
            dev.SvrAddr = SvrAddr;
            //dev.SwitchFile = SwitchFile;
            return dev;
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            string str = reader.ReadString();
            XmlDocument doc = new XmlDocument();
            StringReader sr = new StringReader(str);

            doc.Load(sr);

            foreach (XmlAttribute attr in doc.ChildNodes[1].Attributes)
            {
                PropertyInfo pi = this.GetType().GetProperty(attr.Name);
                if (pi == null) continue;
                if (pi.PropertyType == typeof(Boolean))
                {
                    bool val = bool.Parse(attr.Value);
                    pi.SetValue(this, val, null);
                }
                else
                {
                    pi.SetValue(this, attr.Value, null);
                }
            }


        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
                XmlNode node = doc.CreateNode(XmlNodeType.Element, "DevInfo", string.Empty);
                doc.AppendChild(node);

                foreach (PropertyInfo pi in this.GetType().GetProperties())
                {
                    object val = pi.GetValue(this, null);
                    XmlAttribute attr = doc.CreateAttribute(pi.Name);
                    attr.Value = val.ToString();
                    node.Attributes.Append(attr);
                }

                //XmlAttribute attr = doc.CreateAttribute("DevType");
                //attr.Value = _devType;
                //node.Attributes.Append(attr);

                //attr = doc.CreateAttribute("DevAddr");
                //attr.Value = DevAddr;
                //node.Attributes.Append(attr);

                //attr = doc.CreateAttribute("Global");
                //attr.Value = Global.ToString();
                //node.Attributes.Append(attr);

                //attr = doc.CreateAttribute("IsRemote");
                //attr.Value = IsRemote.ToString();
                //node.Attributes.Append(attr);

                //attr = doc.CreateAttribute("SvrAddr");
                //attr.Value = SvrAddr;
                //node.Attributes.Append(attr);

                //attr = doc.CreateAttribute("SwitchFile");
                //attr.Value = _switchFile;
                //node.Attributes.Append(attr);

                StringWriter sw = new StringWriter();
                doc.Save(sw);

                writer.WriteString(sw.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Initialize(out MessageInstruments instr)
        {
            instr = InstrumentFactory.CreateInstanceFromDisplayName<MessageInstruments>(
                _devType);
            if (_global)
            {
                if (Settings.Instance.IsRemoteDriverType)
                {
                    if (!instr.Initialize(_devAddr, Settings.Instance.InstrumentsServerIp))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!instr.Initialize(_devAddr))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (_isRemote)
                {
                    if (!instr.Initialize(_devAddr, _svrAddr))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!instr.Initialize(_devAddr))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion
    }
}
