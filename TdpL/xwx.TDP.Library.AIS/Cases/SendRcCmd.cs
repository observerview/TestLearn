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
    [Category("遥控遥测"),
     DisplayName("发射遥控指令"),
     Description("通过tcp通讯地检软件，发射遥控指令。"),
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
            [Category("A.指令参数"),
             DisplayName("A01.遥控指令"),
             Description("遥控指令，16进制字符串，8字节。必须用空格分开"),
             DefaultValue(typeof(string), "0A A1 FF FF FF FF FF FF"),
            ]
            public string RcCmdString
            {
                get { return _rcCmdString; }
                set { _rcCmdString = value; }
            }

            private string _rcCmdName = "指令名称";
            [Category("A.指令参数"),
             DisplayName("A02.遥控指令名称"),
             Description("遥控指令功能的名字，会显示在测试过程的log里面，需要和上面的指令对应"),
             DefaultValue(typeof(string), "指令名称"),
            ]
            public string RcCmdName
            {
                get { return _rcCmdName; }
                set { _rcCmdName = value; }
            }

            private string _rcCmdHead = "EB 90";
            [Category("A.指令参数"),
             DisplayName("A03.遥控指令同步头"),
             Description("遥控指令同步头，16进制字符串，2字节,。必须用空格分开"),
             DefaultValue(typeof(string), "EB 90"),
            ]
            public string RcCmdHead
            {
                get { return _rcCmdHead; }
                set { _rcCmdHead = value; }
            }

            private string _rcCmdNo = "00";
            [Category("A.指令参数"),
             DisplayName("A04.遥控指令编号"),
             Description("遥控指令编号，16进制字符串，1字节"),
             DefaultValue(typeof(string), "00"),
            ]
            public string RcCmdNo
            {
                get { return _rcCmdNo; }
                set { _rcCmdNo = value; }
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
             Description("配置需要监控的地检程序数量。在地检程序中，需要端口号和它对应。也可以拉多个case，这里都配置1，修改上面的端口号。"),
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

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {
            // 在此处添加测试前的准备工作
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
                        LogInfo(LogInfoType.Notify, $"连接到地测软件成功。ip={_parameterSetting.IpAddr},port= {_parameterSetting.IpPort + (i - 1)}");
                        NetworkStream stream = tcpClient.GetStream();
                        stream.ReadTimeout = readTimeoutMs;

                        byte[] data = dataTransformation(command);

                        //send command
                        stream.Write(data, 0, data.Length);

                        LogInfo(LogInfoType.Log, $"发送指令:{_parameterSetting.RcCmdName},指令码：{_parameterSetting.RcCmdString} ");

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
                                LogInfo(LogInfoType.Log, "成功返回同步 D5 C8, 命令发送成功");
                                LogResult($"设备{i} - {_parameterSetting.RcCmdName}_命令发送", "PASS", "", new ValueLimit("PASS"));
                            }
                            else
                            {
                                LogInfo(LogInfoType.Log, "命令发送失败");
                                LogResult($"设备{i} - {_parameterSetting.RcCmdName}_命令发送", "FAIL", "", new ValueLimit("PASS"));
                                return ExecOkError.Error;
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
                    LogInfo(LogInfoType.Error, "连接到地测软件失败。");
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

        //处理发送数 时将内容转换为合适的编码格式数据
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