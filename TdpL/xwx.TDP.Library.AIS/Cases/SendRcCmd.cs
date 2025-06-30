using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.AIS.Devs;
using xwx.TDP.Library.Common;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("ң��ң��"),
     DisplayName("����ң��ָ��"),
     Description("ͨ��tcpͨѶ�ؼ����������ң��ָ�"),
    ]
    public class SendRcCmd : CoreCaseCommon
    {
        #region Private Fields
        private string _idn = "";
        int readTimeoutMs = 5000;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            
            private string _rcCmdString = "0A A1 FF FF FF FF FF FF";
            [Category("A.ָ�����"),
             DisplayName("A01.ң��ָ��"),
             Description("ң��ָ�16�����ַ�����8�ֽڡ������ÿո�ֿ�"),
             DefaultValue(typeof(string), "0A A1 FF FF FF FF FF FF"),
            ]
            public string RcCmdString
            {
                get { return _rcCmdString; }
                set { _rcCmdString = value; }
            }

            private string _rcCmdName = "ָ������";
            [Category("A.ָ�����"),
             DisplayName("A02.ң��ָ������"),
             Description("ң��ָ��ܵ����֣�����ʾ�ڲ��Թ��̵�log���棬��Ҫ�������ָ���Ӧ"),
             DefaultValue(typeof(string), "ָ������"),
            ]
            public string RcCmdName
            {
                get { return _rcCmdName; }
                set { _rcCmdName = value; }
            }

            private string _rcCmdHead = "EB 90";
            [Category("A.ָ�����"),
             DisplayName("A03.ң��ָ��ͬ��ͷ"),
             Description("ң��ָ��ͬ��ͷ��16�����ַ�����2�ֽ�,�������ÿո�ֿ�"),
             DefaultValue(typeof(string), "EB 90"),
            ]
            public string RcCmdHead
            {
                get { return _rcCmdHead; }
                set { _rcCmdHead = value; }
            }

            private string _rcCmdNo = "00";
            [Category("A.ָ�����"),
             DisplayName("A04.ң��ָ����"),
             Description("ң��ָ���ţ�16�����ַ�����1�ֽ�"),
             DefaultValue(typeof(string), "00"),
            ]
            public string RcCmdNo
            {
                get { return _rcCmdNo; }
                set { _rcCmdNo = value; }
            }

            private string _ipAddr = "127.0.0.1";
            [Category("B.�������"),
             DisplayName("B01.IP��ַ"),
             Description("ң��ң�����IP��ַ"),
             DefaultValue(typeof(string), "127.0.0.1"),
            ]
            public string IpAddr
            {
                get { return _ipAddr; }
                set { _ipAddr = value; }
            }

            private int _ipPort = 1790;
            [Category("B.�������"),
             DisplayName("B01.�˿ں�"),
             Description("ң��ң�����˿ں�,����ж������ˣ��������Ϊ��ʼ�˿ںţ�����������+1"),
             DefaultValue(typeof(int), "1790"),
            ]
            public int IpPort
            {
                get { return _ipPort; }
                set { _ipPort = value; }
            }

            private int _dJNumber = 1;
            [Category("C.ͬʱ��������"),
             DisplayName("B01.����"),
             Description("������Ҫ��صĵؼ�����������ڵؼ�����У���Ҫ�˿ںź�����Ӧ��Ҳ���������case�����ﶼ����1���޸�����Ķ˿ںš�"),
             DefaultValue(typeof(int), "1"),
            ]
            public int DJNumber
            {
                get { return _dJNumber; }
                set { _dJNumber = value; }
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

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {
            // �ڴ˴���Ӳ���ǰ��׼������
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            string command = _parameterSetting.RcCmdHead + " "+ _parameterSetting.RcCmdNo + " " + _parameterSetting.RcCmdString;
            for (int i = 1; i <= _parameterSetting.DJNumber; i++)
            {
                try
                {
                    using (TcpClient tcpClient = new TcpClient(_parameterSetting.IpAddr, _parameterSetting.IpPort + (i - 1)))
                    {
                        LogInfo(LogInfoType.Notify, $"���ӵ��ز�����ɹ���ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
                        NetworkStream stream = tcpClient.GetStream();
                        stream.ReadTimeout = readTimeoutMs;

                        byte[] data = dataTransformation(command);

                        //send command
                        stream.Write(data, 0, data.Length);

                        LogInfo(LogInfoType.Log, $"����ָ��:{_parameterSetting.RcCmdName},ָ���룺{_parameterSetting.RcCmdString} ");

                        byte[] returnData = new byte[256];
                        try
                        {
                            int _length = stream.Read(data, 0, data.Length);
                            string reData = Encoding.Default.GetString(data, 0, _length);
                            byte[] reDataByte = Encoding.Default.GetBytes(reData);
                            StringBuilder sb = new StringBuilder();
                            for (int j = 0; j < reDataByte.Length; j++)
                            {
                                sb.AppendFormat("{0:x2}" + " ", data[j]);
                            }
                            if (returnData != null && sb.ToString().ToUpper().Contains("D5 C8"))
                            {
                                LogInfo(LogInfoType.Log, "�ɹ�����ͬ�� D5 C8, ����ͳɹ�");
                                LogResult($"�豸{i} - {_parameterSetting.RcCmdName}_�����", "PASS", "", new ValueLimit("PASS"));
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, "�����ʧ��");
                                LogResult($"�豸{i} - {_parameterSetting.RcCmdName}_�����", "FAIL", "", new ValueLimit("PASS"));
                                return ExecOkError.Error;
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "��ȡ����ֵ���� " + ex.ToString());
                            return ExecOkError.Error;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(LogInfoType.Error, "���ӵ��ز����ʧ�ܡ�");
                    return ExecOkError.Error;
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

        //�������� ʱ������ת��Ϊ���ʵı����ʽ����
        private byte[] dataTransformation(string textdata)
        {
            byte[] data = null;
            string[] HexStr = textdata.Trim().Split(' ');
            data = new byte[HexStr.Length];
            for (int i = 0; i < HexStr.Length; i++)
            {
                data[i] = (byte)(Convert.ToInt32(HexStr[i], 16));
            }  
            return data;
        }
    }
}