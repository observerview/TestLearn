//
using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;
using NationalInstruments.Visa;
using System.Threading;
using System.IO;
using Ivi.Visa;
using System.Text;
using System.Text.RegularExpressions;

namespace xwx.TDP.Library.DTC.Cases
{
    [Category("仪表控制"),
     DisplayName("获取N5222B测试data"),
     Description("获取N5222B测试data"),
    ]
    public class GetN5222BData : CoreCaseCommon
    {
        #region Private Fields
        private MessageBasedSession mbSession;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            private string _visaAddress = "TCPIP0::192.168.0.11::INSTR";
            [Category("A.仪表参数组"),
             DisplayName("A01.仪器VISA地址"),
             Description("支持visa的仪器的IP地址,如TCPIP0::192.168.0.11::INSTR"),
             DefaultValue(typeof(string), "TCPIP0::192.168.0.11::INSTR"),
            ]
            public string VisaAddress
            {
                get { return _visaAddress; }
                set { _visaAddress = value; }
            }

            private int _cmdTimeSpan = 50;
            [Category("A.仪表参数组"),
             DisplayName("A02.VISA命令发送间隔"),
             Description("VISA命令发送间隔，单位毫秒ms"),
             DefaultValue(typeof(int), "50"),
            ]
            public int CmdTimeSpan
            {
                get { return _cmdTimeSpan; }
                set { _cmdTimeSpan = value; }
            }

            private string _s2pFilePath = @"D:/Test2025/Data";
            [Category("B.测试信息"),
             DisplayName("B01.仪表内Data存储路径"),
             Description("B01.仪表内Data存储路径，s2p文件，这是仪表内的存储路径,只要路径，不要名称，名称按规则生成"),
             DefaultValue(typeof(string), @"D:/Test2025/Data"),
            ]
            public string S2pFilePath
            {
                get { return _s2pFilePath; }
                set { _s2pFilePath = value; }
            }

            private string _s2pLocalFilePath = @"C:/AutoTest/Logs/DTC";
            [Category("B.测试信息"),
             DisplayName("B02.测试机Data存储路径（.s2p）"),
             Description("B02.测试机Data存储路径，s2p文件，这是仪表内的存储路径"),
             DefaultValue(typeof(string), @"C:/AutoTest/Logs/DTC"),
            ]
            public string S2pLocalFilePath
            {
                get { return _s2pLocalFilePath; }
                set { _s2pLocalFilePath = value; }
            }

            private string _dataFolderName = @"ZB_RF_#1";
            [Category("B.测试信息"),
             DisplayName("B02.测试Data存储文件夹名称"),
             Description("B02.测试Data存储文件夹名称，仪表和本地相同，注意要可区分，最终文件夹会加上时间信息"),
             DefaultValue(typeof(string), @"ZB_RF_#1"),
            ]
            public string DataFolderName
            {
                get { return _dataFolderName; }
                set { _dataFolderName = value; }
            }

            private string _s2pFileName = @"TX1";
            [Category("B.测试信息"),
             DisplayName("B02.测试Data文件名称"),
             Description("B02.测试Data文件名称，仪表和本地相同，注意要可区分，最终文件夹会加上时间信息"),
             DefaultValue(typeof(string), @"TX1"),
            ]
            public string S2pFileName
            {
                get { return _s2pFileName; }
                set { _s2pFileName = value; }
            }




            //
            private string _displayName = "通用发VISA指令并处理返回值";

            [DisplayName("Z01. 显示名称")]
            [Category("Z. 测试case通用配置")]
            [DefaultValue("获取N5222B测试data")]
            [Description("测试case通用配置，修改这个可以修改测试项在sequence里面的显示名称")]
            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
                set
                {
                    _displayName = (string.IsNullOrEmpty(value) ? "获取N5222B测试data" : value);
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

        public GetN5222BData() : this("获取N5222B测试data")
        {
        }

        public GetN5222BData(string displayName)
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
            string testTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            string tempFilePath = Path.Combine(_parameterSetting.S2pFilePath,testTime + "_"+ _parameterSetting.DataFolderName);//仪表端

            string localFilePath = Path.Combine(_parameterSetting.S2pLocalFilePath, testTime + "_" + _parameterSetting.DataFolderName);//测试机

            using (var rmSession = new ResourceManager())
            {
                mbSession = (MessageBasedSession)rmSession.Open(_parameterSetting.VisaAddress,AccessModes.None,10000);
                //检查并创建目录 ： MMEM:MDIR "xxx"
                string directory = tempFilePath;//.Substring(0, tempFilePath.LastIndexOf('/'));
                try
                {
                    // 尝试列出目录内容，检查目录是否存在
                    mbSession.RawIO.Write($"MMEM:CAT? \"{directory}\"");
                    mbSession.RawIO.ReadString(); // 读取目录内容,如果没有会报错
                }
                catch
                {
                    // 如果目录不存在，创建目录
                    mbSession.RawIO.Write($"MMEM:MDIR \"{directory}\"");
                }

                //生成结果文件
                string datafile = directory + "/" + _parameterSetting.S2pFileName+".s2p";
                string _cmdSaveData = string.Format("MMEM:STOR '{0}'", datafile);
                mbSession.RawIO.Write(_cmdSaveData);
                Thread.Sleep(_parameterSetting.CmdTimeSpan);
                //文件传本地
                string _cmdTransferToLocal = string.Format($"MMEM:TRAN? \"{datafile}\"");
                mbSession.RawIO.Write(_cmdTransferToLocal);
                LogInfo(LogInfoType.Normal, $"发送VISA指令：{_cmdTransferToLocal}", "");

                // 读取 SCPI 头部信息
                StringBuilder headerBuilder = new StringBuilder();
                byte[] buffer = new byte[1];
                int maxCount = 0;
                byte StartByte = 0;
                while (true)
                {
                    maxCount++;
                    buffer = mbSession.RawIO.Read(1);
                    char currentChar = (char)buffer[0];
                    headerBuilder.Append(currentChar);
                    // 头部信息以叹号 '!' 结束
                    if (currentChar == '!' || maxCount >= 100)
                    {
                        StartByte = buffer[0];
                        break;
                    }
                }
                // 解析头部信息
                string header = headerBuilder.ToString();
                Match match = Regex.Match(header, @"#(\d+)!");
                if (!match.Success)
                {
                    LogInfo(LogInfoType.Error,"Invalid SCPI header format.返回的数据格式不正确，测试结束，请检查命令和环境");
                    return ExecOkError.Error;
                }
                // 获取数据长度
                long dataLength = Int64.Parse(match.Groups[1].Value);

                // 读取二进制数据
                byte[] fileData = new byte[dataLength];
                fileData = mbSession.RawIO.Read(dataLength);

                //前面的叹号！在上次就被取走了需要补回
                byte[] resultData = new byte[fileData.Length+1];
                resultData[0] = StartByte;
                Array.Copy(fileData, 0, resultData, 1, fileData.Length);


                //? mbSession.RawIO.ReadString(dataLength);

                Thread.Sleep(_parameterSetting.CmdTimeSpan);

                // 指定本地保存路径
                string localFile = localFilePath  +"/" + _parameterSetting.S2pFileName + ".s2p";  // 修改为本地路径

                // 将数据保存到本地文件
                File.WriteAllBytes(localFile, resultData);

                mbSession.RawIO.ReadString();

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
