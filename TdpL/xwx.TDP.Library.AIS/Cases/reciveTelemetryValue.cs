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
    [Category("遥控遥测"),
     DisplayName("接收遥测结果,判断返回数值"),
     Description("通过tcp通讯地检软件，接收遥测结果。"),
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
            [Category("A.指令参数"),
             DisplayName("A01.遥测指令编号"),
             Description("遥测指令编号，填10进制，1字节.后面传递的时候会转16进制字符串 "),
             DefaultValue(typeof(string), "91"),
            ]
            public string RcCmdNo
            {
                get { return _rcCmdNo; }
                set { _rcCmdNo = value; }
            }

            private string _rcCmdName = "AIS1接收包计数";
            [Category("A.指令参数"),
             DisplayName("A02.遥控指令"),
             Description("遥测指令功能的名字，会显示在测试过程的log里面，需要和上面的指令对应"),
             DefaultValue(typeof(string), "指令名称"),
            ]
            public string RcCmdName
            {
                get { return _rcCmdName; }
                set { _rcCmdName = value; }
            }

            private string _rcCmdHead = "EB 90";
            [Category("A.指令参数"),
             DisplayName("A03.遥测指令同步头"),
             Description("遥测指令同步头，16进制字符串，2字节,。必须用空格分开"),
             DefaultValue(typeof(string), "EB 90"),
            ]
            public string RcCmdHead
            {
                get { return _rcCmdHead; }
                set { _rcCmdHead = value; }
            }

            private string _rcCmdString = "FF FF FF FF FF FF FF FF";
            [Category("A.指令参数"),
             DisplayName("A04.遥测指令"),
             Description("遥测指令，16进制字符串，8字节。必须用空格分开"),
             DefaultValue(typeof(string), "FF FF FF FF FF FF FF FF"),
            ]
            public string RcCmdString
            {
                get { return _rcCmdString; }
                set { _rcCmdString = value; }
            }


            private string _ipAddr = "127.0.0.1";
            [Category("B.服务参数"),
             DisplayName("B01.IP地址"),
             Description("遥控遥测服务IP地址"),
             DefaultValue(typeof(string), "127.0.0.1"),
            ]
            public string IpAddr
            {
                get { return _ipAddr; }
                set { _ipAddr = value; }
            }

            private int _ipPort = 1790;
            [Category("B.服务参数"),
             DisplayName("B01.端口号"),
             Description("遥控遥测服务端口号,如果有多个服务端，这个号作为起始端口号，其他的依次+1"),
             DefaultValue(typeof(int), "1790"),
            ]
            public int IpPort
            {
                get { return _ipPort; }
                set { _ipPort = value; }
            }

            private int _dJNumber = 1;
            [Category("C.同时测试数量"),
             DisplayName("B01.数量"),
             Description("配置需要监控的地检程序数量。在地检程序中，需要端口号和它对应。"),
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
            // 限制值示例
            private ValueLimit _limitExample = new ValueLimit("[0,10000] | double");
            [Category("A.限制值组"),
             DisplayName("A01.限制值名称"),
             Description("限制值描述"),
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
            // 在此处添加测试前的准备工作
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
                        LogInfo(LogInfoType.Notify, $"连接到地测软件成功。ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
                        NetworkStream stream = tcpClient.GetStream();
                        stream.ReadTimeout = readTimeoutMs;

                        byte[] data = dataTransformation(command);
                        byte[] satelliteNumber = dataTransformation(satelliteNumberCmd);
                        byte[] boardMode = dataTransformation(boardModeCmd);
                        //获取卫星编号
                        stream.Write(satelliteNumber, 0, satelliteNumber.Length);
                        byte[] returnSatelliteNumberData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnSatelliteNumberData, 0, returnSatelliteNumberData.Length);
                            string reSatelliteNumberData = Encoding.Default.GetString(returnSatelliteNumberData, 0, _length);

                            if (returnSatelliteNumberData != null && reSatelliteNumberData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Error, "返回默认值 FF FF, 命令发送成功,但是未能获取到卫星编号。测试停止");
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"发送指令,获取当前卫星编号是:{reSatelliteNumberData}");
                                SatelliteNumberString = reSatelliteNumberData;
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "读取返回值报错： " + ex.ToString());
                            return ExecOkError.Error;
                        }
                        //获取主机/备机信息
                        stream.Write(boardMode, 0, boardMode.Length);
                        byte[] returnboardModeData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnboardModeData, 0, returnboardModeData.Length);
                            string reboardModeData = Encoding.Default.GetString(returnboardModeData, 0, _length);

                            if (returnboardModeData != null && returnboardModeData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Error, "返回默认值 FF FF, 命令发送成功,但是未能获取到卫星编号。测试停止");
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"发送指令,获取当前卫星编号是:{reboardModeData}");
                                boardModeString = reboardModeData;
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "读取返回值报错： " + ex.ToString());
                            return ExecOkError.Error;
                        }


                        //send command
                        stream.Write(data, 0, data.Length);
                        LogInfo(LogInfoType.Log, $"发送指令获取遥测量：{_parameterSetting.RcCmdName},编号：0x{_parameterSetting.RcCmdString} ");
                        byte[] returnData = new byte[256];
                        try
                        {
                            int _length = stream.Read(returnData, 0, returnData.Length);
                            string reData = Encoding.Default.GetString(returnData, 0, _length);

                            if (returnData != null && reData.ToString().ToUpper().Contains("FFFF"))
                            {
                                LogInfo(LogInfoType.Log, "返回默认值 FF FF, 命令发送成功,但是未获取到对应的遥测量。使用异常值-100作为结果");
                                LogResult($"{_parameterSetting.RcCmdName}_遥测量", -100, "-100", "工程值", _limitSetting.LimitExample);
                                return ExecOkError.Error;
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, $"遥测量返回{reData.ToString().ToUpper()}");
                                int rValue = Convert.ToInt32(reData.ToString().ToUpper());

                                LogResult($"卫星编号:{SatelliteNumberString} - 主备机：{boardModeString} - {_parameterSetting.RcCmdName}_遥测量", rValue, rValue.ToString(), "工程值", _limitSetting.LimitExample);
                                //UserRunTimeVarPool.Add("卫星编号", SatelliteNumberString);
                                //UserRunTimeVarPool.Add("主备机", boardModeString);
                            }
                        }
                        catch (IOException ex)
                        {
                            LogInfo(LogInfoType.Log, "读取返回值报错： " + ex.ToString());
                            return ExecOkError.Error;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogInfo(LogInfoType.Error, $"连接到地测软件失败。ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
                    return ExecOkError.Error;
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