using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("融为数传"),
     DisplayName("数传测试"),
     Description("监听设备3075端口，测试程序读取并分析判断结果，并且保存一个本地的数据"),
    ]
    public class RongweiHDR : CoreCaseCommon
    {
        #region Private Fields
        //private Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private TcpClient tcpClient;
        private TcpClient tcpClient_aischeck;
        //creat .dat file
        string filename = string.Empty;
        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        Thread thrRecv;
        private bool isReceiving = false;

        private int Ais1Count = 0;
        private int Ais2Count = 0;
        private int Lais1Count = 0;
        private int Lais2Count = 0;
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            // 参数示例
            private string _ipAddr = "192.168.1.51";
            [Category("A.融为数传设备参数"),
             DisplayName("A01.IP地址"),
             Description("融为数传设备IP地址"),
             DefaultValue(typeof(double), "192.168.1.51"),
            ]
            public string IpAddr
            {
                get { return _ipAddr; }
                set { _ipAddr = value; }
            }

            private int _ipPort = 3075;
            [Category("A.融为数传设备参数"),
             DisplayName("A02.端口号"),
             Description("融为数传设备服务端口号"),
             DefaultValue(typeof(int), "3075"),
            ]
            public int IpPort
            {
                get { return _ipPort; }
                set { _ipPort = value; }
            }

            private string _logDirectory = @"C://Autotest//Logs";
            [Category("B.测试参数"),
             DisplayName("B01.数据dat存储路径"),
             Description("数据dat存储路径"),
             DefaultValue(typeof(double), @"C://Autotest//Logs"),
            ]
            public string LogDirectory
            {
                get { return _logDirectory; }
                set { _logDirectory = value; }
            }

            private int _receiveTime = 60000;
            [Category("B.测试参数"),
             DisplayName("B01.收数时间 ms"),
             Description("收数时间 毫秒"),
             DefaultValue(typeof(int), "60000"),
            ]
            public int ReceiveTime
            {
                get { return _receiveTime; }
                set { _receiveTime = value; }
            }

        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // 限制值示例
            private ValueLimit _ais1CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.接收帧计数"),
             DisplayName("A01.AIS1"),
             Description("AIS1接收帧计数"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Ais1CountLimit
            {
                get { return _ais1CountLimit; }
                set { _ais1CountLimit = value; }
            }
            private ValueLimit _ais2CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.接收帧计数"),
             DisplayName("A01.AIS2"),
             Description("AIS2接收帧计数"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Ais2CountLimit
            {
                get { return _ais2CountLimit; }
                set { _ais2CountLimit = value; }
            }

            private ValueLimit _lais1CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.接收帧计数"),
             DisplayName("A01.LAIS1"),
             Description("LAIS1接收帧计数"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Lais1CountLimit
            {
                get { return _lais1CountLimit; }
                set { _lais1CountLimit = value; }
            }

            private ValueLimit _lais2CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.接收帧计数"),
             DisplayName("A01.LAIS2"),
             Description("LAIS2接收帧计数"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Lais2CountLimit
            {
                get { return _lais2CountLimit; }
                set { _lais2CountLimit = value; }
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
            string wxbh = string.Format("卫星：");
            try
            {
                wxbh = wxbh + UserRunTimeVarPool["卫星"].ToString();
                LogInfo(LogInfoType.Error, string.Format("开始 {} 的数传测试", wxbh));
            }
            catch (Exception)
            {
                wxbh = wxbh + "获取卫星编号失败";
                LogInfo(LogInfoType.Error, wxbh);
                return ExecOkError.Error;
            }

            filename = Path.Combine(_parameterSetting.LogDirectory, "AIS_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+".dat");
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                // 创建dat文件
            }
            try
            {
                //开始监听收数
                StartReceive(false,_parameterSetting.IpAddr,_parameterSetting.IpPort);
                //这里应该去遥测检查是否数传包不在变化，这里先用延时代替。
                Thread.Sleep(_parameterSetting.ReceiveTime);
                //关闭监听收数
                StartReceive(true, _parameterSetting.IpAddr, _parameterSetting.IpPort);
                //处理结果，通过调用pc的工具
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ExecOkError.Error;
            }

            // 处理数据
            aisDataCheck(filename);

            LogResult($"{wxbh}- AIS1 共接收帧数", Ais1Count, Ais1Count.ToString(), "帧", _limitSetting.Ais1CountLimit);
            LogResult($"{wxbh}- AIS2 共接收帧数", Ais2Count, Ais2Count.ToString(), "帧", _limitSetting.Ais2CountLimit);
            LogResult($"{wxbh}- LAIS1 共接收帧数", Lais1Count, Lais1Count.ToString(), "帧", _limitSetting.Lais1CountLimit);
            LogResult($"{wxbh}- LAIS2 共接收帧数", Lais2Count, Lais2Count.ToString(), "帧", _limitSetting.Lais2CountLimit);

            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            // 在此处添加测试后的清理工作
            
            return ExecOkError.OK;
        }
        #endregion

        private void aisDataCheck(string datafileName)
        {
            // 正则表达式，用于匹配帧数
            Regex frameRegex = new Regex(@"共接收(\d+)帧");

            string cmd = "datpath=" + datafileName;
            sendCmdtoCheck(cmd);
            var resultList = File.ReadAllLines(@"C:\AutoTest\Logs\RW_HDR_last_result.txt");
            if(resultList.Length < 10)
            {
                sendCmdtoCheck(cmd);
                resultList = File.ReadAllLines(@"C:\AutoTest\Logs\RW_HDR_last_result.txt");
            }
            foreach(var result in resultList)
            {
                if (result.Contains("AIS1"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // 提取帧数
                        Ais1Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("AIS2"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // 提取帧数
                        Ais2Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("LAIS1"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // 提取帧数
                        Lais1Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("LAIS2"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // 提取帧数
                        Lais2Count = int.Parse(match.Groups[1].Value);
                    }
                }
            }

        }
        private bool sendCmdtoCheck(string cmd)
        {
            byte[] data = new ASCIIEncoding().GetBytes(cmd);
            try
            {
                StartReceive(false, "127.0.0.1", 12345);
                tcpClient.GetStream().Write(data, 0, data.Length);
                Thread.Sleep(5000);
                StartReceive(true, "127.0.0.1", 12345);
            }
            catch (Exception)
            {
                LogInfo(LogInfoType.Warning, "发送tcp到checkais失败，请检查CheckAis.exe程序是否已经打开");
            }
            return true;
        }

        
        /// <summary>
        /// Start a udpClient to listen
        /// </summary>
        /// <param name="isMonitor">if true, monitor stop</param>
        private void StartReceive(bool isMonitor,string hostIp, int hostPort)
        {
            if (!isMonitor) // 未监听的情况，开始监听
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(hostIp, hostPort);

                isReceiving = true;
                thrRecv = new Thread(ReceiveMessage)
                {
                    IsBackground = true
                };
                thrRecv.Start();
                LogInfo(LogInfoType.Normal, "tcp监听器已启动,开始监听数传设备转发的数据");
            }
            else // 正在监听的情况，终止监听
            {
                isReceiving=false;
                //thrRecv?.Join(); // 等待线程结束，确保线程安全退出
                thrRecv.Abort();//必须先关闭这个线程，否则会异常
                tcpClient.Close();
                LogInfo(LogInfoType.Normal, "tcp监听器已关闭，停止监听数传设备");
            }

        }
        void ReceiveMessage()
        {
            NetworkStream reciveStream = ((TcpClient)tcpClient).GetStream();
            while (isReceiving)
            {
                try
                {
                    byte[] bytRecv = new byte[1040*8];
                    int datalength =  reciveStream.Read(bytRecv,0,bytRecv.Length);
                    //byte[] bytRecv = new byte[1024];
                    LogInfo(LogInfoType.Normal, $"Received data from {_parameterSetting.IpPort}: {bytRecv.Length} bytes");
                    //write to file
                    SaveDataToFile(filename, bytRecv);
                }
                catch(Exception ex)
                {
                    LogInfo(LogInfoType.Error, $"Error receiving UDP data: {ex.Message}");
                }

            }
            tcpClient.Close();
            LogInfo(LogInfoType.Normal, "Receiver thread stopped.");

        }

        private void SaveDataToFile(string fileName, byte[] data)
        {
            try
            {
                // 使用 FileStream 以追加模式打开文件，并将数据写入
                using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length); // 将接收到的数据写入文件
                }
                LogInfo(LogInfoType.Normal, $"{data.Length} bytes Data saved to {filename}");
            }
            catch (Exception ex)
            {
                LogInfo(LogInfoType.Error, $"Error saving data to file: {ex.Message}");
            }
        }

    }
}