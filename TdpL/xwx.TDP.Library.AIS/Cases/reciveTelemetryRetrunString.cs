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
     DisplayName("读取遥测结果存入变量给后面使用"),
     Description("通过tcp通讯地检软件，接收遥测结果，并存到UserRunTimeVarPool供后续测试case使用"),
    ]
    public class reciveTelemetryRetrunString : CoreCaseCommon
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

            private string _rcCmdName = "MCU镜像保存状态";
            [Category("A.指令参数"),
             DisplayName("A02.遥控指令"),
             Description("遥测指令功能的名字，会显示在测试过程的log里面，需要和上面的指令对应"),
             DefaultValue(typeof(string), "改成需要的指令名称"),
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

            private string _name = "ID1";
            [Category("B.变量参数"),
             DisplayName("B01.变量名"),
             Description("这个变量名作为后续其他查询值的查询名称。"),
             DefaultValue(typeof(string), "ID1"),
            ]
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }


            private string _ipAddr = "127.0.0.1";
            [Category("C.服务参数"),
             DisplayName("C01.IP地址"),
             Description("遥控遥测服务IP地址"),
             DefaultValue(typeof(string), "127.0.0.1"),
            ]
            public string IpAddr
            {
                get { return _ipAddr; }
                set { _ipAddr = value; }
            }

            private int _ipPort = 1791;
            [Category("C.服务参数"),
             DisplayName("C01.IP地址"),
             Description("遥控遥测服务IP地址"),
             DefaultValue(typeof(int), "1791"),
            ]
            public int IpPort
            {
                get { return _ipPort; }
                set { _ipPort = value; }
            }
        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // 限制值示例
            //private ValueLimit _limitValue = new ValueLimit("输入判定用字符串 | string");
            //[Category("A.结果限制值组"),
            // DisplayName("A01.返回值字符串"),
            // Description("这个case的返回值是字符串，如果返回值和这里设置的字符串一直，则判定pass"),
            // DefaultValue(typeof(string), "输入判定用字符串"),
            //]
            //public ValueLimit LimitValue
            //{
            //    get { return _limitValue; }
            //    set { _limitValue = value; }
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
            string command = _parameterSetting.RcCmdHead + " " + Convert.ToString(Convert.ToInt32(_parameterSetting.RcCmdNo), 16) + " " + _parameterSetting.RcCmdString;

            try
            {
                using (TcpClient tcpClient = new TcpClient(_parameterSetting.IpAddr, _parameterSetting.IpPort))
                {
                    NetworkStream stream = tcpClient.GetStream();
                    stream.ReadTimeout = readTimeoutMs;

                    byte[] data = dataTransformation(command);

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
                            LogResult($"{_parameterSetting.RcCmdName}_遥控量获取", -100, "-100", "string", new ValueLimit("{true} | bool"));
                            return ExecOkError.Error;
                        }
                        else
                        {
                            LogInfo(LogInfoType.Log, $"遥测量返回 : {reData.ToString().ToUpper()}");
                            string rValue = reData.ToString();
                            UserRunTimeVarPool.Add(_parameterSetting.Name, rValue);
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