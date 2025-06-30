using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("ң��ң��"),
     DisplayName("����ң����,�жϷ�����ֵ"),
     Description("ͨ��tcpͨѶ�ؼ����������ң������"),
    ]
    public class reciveTelemetryValue : CoreCaseCommon
    {
        #region Private Fields
        private string _idn = "";
        int readTimeoutMs = 5000;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            private string _rcCmdNo = "91";
            [Category("A.ָ�����"),
             DisplayName("A01.ң��ָ����"),
             Description("ң��ָ���ţ���10���ƣ�1�ֽ�.���洫�ݵ�ʱ���ת16�����ַ��� "),
             DefaultValue(typeof(string), "91"),
            ]
            public string RcCmdNo
            {
                get { return _rcCmdNo; }
                set { _rcCmdNo = value; }
            }

            private string _rcCmdName = "AIS1���հ�����";
            [Category("A.ָ�����"),
             DisplayName("A02.ң��ָ��"),
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

            private string _rcCmdString = "FF FF FF FF FF FF FF FF";
            [Category("A.ָ�����"),
             DisplayName("A04.ң��ָ��"),
             Description("ң��ָ�16�����ַ�����8�ֽڡ������ÿո�ֿ�"),
             DefaultValue(typeof(string), "FF FF FF FF FF FF FF FF"),
            ]
            public string RcCmdString
            {
                get { return _rcCmdString; }
                set { _rcCmdString = value; }
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
             Description("������Ҫ��صĵؼ�����������ڵؼ�����У���Ҫ�˿ںź�����Ӧ��"),
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
            private ValueLimit _limitExample = new ValueLimit("[0,10000] | double");
            [Category("A.����ֵ��"),
             DisplayName("A01.����ֵ����"),
             Description("����ֵ����"),
             DefaultValue(typeof(ValueLimit), "[0,10000] | double"),
            ]
            public ValueLimit LimitExample
            {
                get { return _limitExample; }
                set { _limitExample = value; }
            }
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

            string command = _parameterSetting.RcCmdHead + " " + Convert.ToString(Convert.ToInt32(_parameterSetting.RcCmdNo),16) + " " + _parameterSetting.RcCmdString;
            string satelliteNumberCmd = _parameterSetting.RcCmdHead + " " + Convert.ToString(23, 16) + " " + _parameterSetting.RcCmdString;
            string boardModeCmd = _parameterSetting.RcCmdHead + " " + Convert.ToString(30, 16) + " " + _parameterSetting.RcCmdString;
            for (int i = 1; i <= _parameterSetting.DJNumber; i++)
            {
                string SatelliteNumberString = string.Empty;
                string boardModeString = string.Empty;
                try
                {
                    using (TcpClient tcpClient = new TcpClient(_parameterSetting.IpAddr, _parameterSetting.IpPort + (i-1)))
                    {
                        LogInfo(LogInfoType.Notify, $"���ӵ��ز�����ɹ���ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
                        NetworkStream stream = tcpClient.GetStream();
                        stream.ReadTimeout = readTimeoutMs;

                        byte[] data = dataTransformation(command);
                        byte[] satelliteNumber = dataTransformation(satelliteNumberCmd);
                        byte[] boardMode = dataTransformation(boardModeCmd);
                        //��ȡ���Ǳ��
                        stream.Write(satelliteNumber, 0, satelliteNumber.Length);
                        byte[] returnSatelliteNumberData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnSatelliteNumberData, 0, returnSatelliteNumberData.Length);
                            string reSatelliteNumberData = Encoding.Default.GetString(returnSatelliteNumberData, 0, _length);

                            if (returnSatelliteNumberData != null && reSatelliteNumberData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Error, "����Ĭ��ֵ FF FF, ����ͳɹ�,����δ�ܻ�ȡ�����Ǳ�š�����ֹͣ");
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"����ָ��,��ȡ��ǰ���Ǳ����:{reSatelliteNumberData}");
                                SatelliteNumberString = reSatelliteNumberData;
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "��ȡ����ֵ���� " + ex.ToString());
                            return ExecOkError.Error;
                        }
                        //��ȡ����/������Ϣ
                        stream.Write(boardMode, 0, boardMode.Length);
                        byte[] returnboardModeData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnboardModeData, 0, returnboardModeData.Length);
                            string reboardModeData = Encoding.Default.GetString(returnboardModeData, 0, _length);

                            if (returnboardModeData != null && returnboardModeData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Error, "����Ĭ��ֵ FF FF, ����ͳɹ�,����δ�ܻ�ȡ�����Ǳ�š�����ֹͣ");
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"����ָ��,��ȡ��ǰ���Ǳ����:{reboardModeData}");
                                boardModeString = reboardModeData;
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "��ȡ����ֵ���� " + ex.ToString());
                            return ExecOkError.Error;
                        }


                        //send command
                        stream.Write(data, 0, data.Length);
                        LogInfo(LogInfoType.Log, $"����ָ���ȡң������{_parameterSetting.RcCmdName},��ţ�0x{_parameterSetting.RcCmdString} ");
                        byte[] returnData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnData, 0, returnData.Length);
                            string reData = Encoding.Default.GetString(returnData, 0, _length);

                            if (returnData != null && reData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Log, "����Ĭ��ֵ FF FF, ����ͳɹ�,����δ��ȡ����Ӧ��ң������ʹ���쳣ֵ-100��Ϊ���");
                                LogResult($"{_parameterSetting.RcCmdName}_ң����", -100, "-100", "����ֵ", _limitSetting.LimitExample);
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"ң��������{reData.ToString().ToUpper()}");
                                int rValue = Convert.ToInt32(reData.ToString().ToUpper());

                                LogResult($"���Ǳ��:{SatelliteNumberString} - ��������{boardModeString} - {_parameterSetting.RcCmdName}_ң����", rValue, rValue.ToString(), "����ֵ", _limitSetting.LimitExample);
                                //UserRunTimeVarPool.Add("���Ǳ��", SatelliteNumberString);
                                //UserRunTimeVarPool.Add("������", boardModeString);
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
                    LogInfo(LogInfoType.Error, $"���ӵ��ز����ʧ�ܡ�ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
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