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
    [Category("�Ǳ����"),
     DisplayName("��VISAָ��"),
     Description("ͨ�ý���VISAָ���Ҫ���õ�ַ����������޷���ֵ"),
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
            [Category("A.����������"),
             DisplayName("A01.����VISA��ַ"),
             Description("֧��visa��������IP��ַ,��TCPIP0::192.168.1.100::INSTR"),
             DefaultValue(typeof(string), "TCPIP0::192.168.1.100::INSTR"),
            ]
            public string VisaAddress
            {
                get { return _visaAddress; }
                set { _visaAddress = value; }
            }

            //private string _visaCmdString = "*IDN?\n";
            //[Category("A.����������"),
            // DisplayName("A02.VISA����"),
            // Description("visa����,��*IDN?\n"),
            // DefaultValue(typeof(string), "*IDN?\n"),
            //]
            //public string VisaCmdString
            //{
            //    get { return _visaCmdString; }
            //    set { _visaCmdString = value; }
            //}

            private string[] _visaCmdStrings = { "*IDN?\n", "" };
            [Category("A.����������"),
             DisplayName("A02.VISA����"),
             Description("visa����,��*IDN?\n"),
             DefaultValue(typeof(string), "*IDN?\n"),
            ]
            public string[] VisaCmdStrings
            {
                get { return _visaCmdStrings; }
                set { _visaCmdStrings = value; }
            }

            private int _cmdTimeSpan = 50;
            [Category("A.����������"),
             DisplayName("A03.VISA����ͼ��"),
             Description("VISA����ͼ������λ����ms"),
             DefaultValue(typeof(int), "50"),
            ]
            public int CmdTimeSpan
            {
                get { return _cmdTimeSpan; }
                set { _cmdTimeSpan = value; }
            }

            //
            private string _displayName = "ͨ�÷�VISAָ��";

            [DisplayName("Z01. ��ʾ����")]
            [Category("Z. ����caseͨ������")]
            [DefaultValue("ͨ�÷�VISAָ��")]
            [Description("����caseͨ�����ã��޸���������޸Ĳ�������sequence�������ʾ����")]
            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
                set
                {
                    _displayName = (string.IsNullOrEmpty(value) ? "ͨ�÷�VISAָ��" : value);
                }
            }
        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // ����ֵʾ��
            //private ValueLimit _limitExample = new ValueLimit("[0,100] | double");
            //[Category("A.����ֵ��"),
            // DisplayName("A01.����ֵ����"),
            // Description("����ֵ����"),
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

        public CommonVisaWrite() : this("ͨ�÷�VISAָ��")
        {
        }

        public CommonVisaWrite(string displayName)
        {
            DisplayName = displayName;
        }

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {
            // �ڴ˴���Ӳ���ǰ��׼������
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            //var dev = new MessageBaseInstrument();
            //dev.Initialize(_parameterSetting.VisaAddress);
            //dev.WriteString("*IDN?\n");
            //string result = string.Empty;
            //dev.ReadString(out result);
            // ע�͵�Ϊ���η�װ�Ľӿڣ�ͨ�ýӿڹ������󡣸�ʱ����ֱ����NI VISA�ӿ�
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
            // �ڴ˴���Ӳ��Ժ��������
            return ExecOkError.OK;
        }
        #endregion
    }
}