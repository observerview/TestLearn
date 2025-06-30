using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;
using NationalInstruments.Visa;
using System.Threading;
using WirelessCommon.Visa;

namespace xwx.TDP.Library.DTC.Cases
{
    [Category("仪表控制"),
     DisplayName("发VISA指令"),
     Description("通用仅发VISA指令，需要设置地址，设置命令，无返回值"),
    ]
    public class CommonVisaWrite : CoreCaseCommon
    {
        #region Private Fields
        private MessageBasedSession mbSession;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            private string _visaAddress = "TCPIP0::192.168.1.100::INSTR";
            [Category("A.参数设置组"),
             DisplayName("A01.仪器VISA地址"),
             Description("支持visa的仪器的IP地址,如TCPIP0::192.168.1.100::INSTR"),
             DefaultValue(typeof(string), "TCPIP0::192.168.1.100::INSTR"),
            ]
            public string VisaAddress
            {
                get { return _visaAddress; }
                set { _visaAddress = value; }
            }

            //private string _visaCmdString = "*IDN?\n";
            //[Category("A.参数设置组"),
            // DisplayName("A02.VISA命令"),
            // Description("visa命令,如*IDN?\n"),
            // DefaultValue(typeof(string), "*IDN?\n"),
            //]
            //public string VisaCmdString
            //{
            //    get { return _visaCmdString; }
            //    set { _visaCmdString = value; }
            //}

            private string[] _visaCmdStrings = { "*IDN?\n", "" };
            [Category("A.参数设置组"),
             DisplayName("A02.VISA命令"),
             Description("visa命令,如*IDN?\n"),
             DefaultValue(typeof(string), "*IDN?\n"),
            ]
            public string[] VisaCmdStrings
            {
                get { return _visaCmdStrings; }
                set { _visaCmdStrings = value; }
            }

            private int _cmdTimeSpan = 50;
            [Category("A.参数设置组"),
             DisplayName("A03.VISA命令发送间隔"),
             Description("VISA命令发送间隔，单位毫秒ms"),
             DefaultValue(typeof(int), "50"),
            ]
            public int CmdTimeSpan
            {
                get { return _cmdTimeSpan; }
                set { _cmdTimeSpan = value; }
            }

            //
            private string _displayName = "通用发VISA指令";

            [DisplayName("Z01. 显示名称")]
            [Category("Z. 测试case通用配置")]
            [DefaultValue("通用发VISA指令")]
            [Description("测试case通用配置，修改这个可以修改测试项在sequence里面的显示名称")]
            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
                set
                {
                    _displayName = (string.IsNullOrEmpty(value) ? "通用发VISA指令" : value);
                }
            }
        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // 限制值示例
            //private ValueLimit _limitExample = new ValueLimit("[0,100] | double");
            //[Category("A.限制值组"),
            // DisplayName("A01.限制值名称"),
            // Description("限制值描述"),
            // DefaultValue(typeof(ValueLimit), "[0,100] | double"),
            //]
            //public ValueLimit LimitExample
            //{
            //    get { return _limitExample; }
            //    set { _limitExample = value; }
            //}
        }
        #endregion

        #region Parameter & Limit Getter and Setter
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get { return _parameterSetting; }
            set
            {
                if (value != null && value is ParameterSetting)
                {
                    _parameterSetting = value as ParameterSetting;
                }
            }
        }

        private LimitSetting _limitSetting = new LimitSetting();
        public override ConfigBase CaseLimitSetting
        {
            get { return _limitSetting; }
            set
            {
                if (value != null && value is LimitSetting)
                {
                    _limitSetting = value as LimitSetting;
                }
            }
        }
        #endregion

        public string DisplayName
        {
            get
            {
                return _parameterSetting.DisplayName;
            }
            set
            {
                _parameterSetting.DisplayName = value;
            }
        }

        public CommonVisaWrite() : this("通用发VISA指令")
        {
        }

        public CommonVisaWrite(string displayName)
        {
            DisplayName = displayName;
        }

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {
            // 在此处添加测试前的准备工作
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            //var dev = new MessageBaseInstrument();
            //dev.Initialize(_parameterSetting.VisaAddress);
            //dev.WriteString("*IDN?\n");
            //string result = string.Empty;
            //dev.ReadString(out result);
            // 注释的为二次封装的接口，通用接口工作量大。赶时间先直接用NI VISA接口
            using (var rmSession = new ResourceManager())
            {
                mbSession = (MessageBasedSession)rmSession.Open(_parameterSetting.VisaAddress);
                foreach(var cmd in _parameterSetting.VisaCmdStrings)
                {
                    mbSession.RawIO.Write(cmd);
                    Thread.Sleep(_parameterSetting.CmdTimeSpan); 
                }
            }
            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            // 在此处添加测试后的清理工作
            return ExecOkError.OK;
        }
        #endregion
    }
}