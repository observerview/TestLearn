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
    [Category("��Ϊ����"),
     DisplayName("��������"),
     Description("�����豸3075�˿ڣ����Գ����ȡ�������жϽ�������ұ���һ�����ص�����"),
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
        /// �̣߳����ϼ���UDP����
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
            // ����ʾ��
            private string _ipAddr = "192.168.1.51";
            [Category("A.��Ϊ�����豸����"),
             DisplayName("A01.IP��ַ"),
             Description("��Ϊ�����豸IP��ַ"),
             DefaultValue(typeof(double), "192.168.1.51"),
            ]
            public string IpAddr
            {
                get { return _ipAddr; }
                set { _ipAddr = value; }
            }

            private int _ipPort = 3075;
            [Category("A.��Ϊ�����豸����"),
             DisplayName("A02.�˿ں�"),
             Description("��Ϊ�����豸����˿ں�"),
             DefaultValue(typeof(int), "3075"),
            ]
            public int IpPort
            {
                get { return _ipPort; }
                set { _ipPort = value; }
            }

            private string _logDirectory = @"C://Autotest//Logs";
            [Category("B.���Բ���"),
             DisplayName("B01.����dat�洢·��"),
             Description("����dat�洢·��"),
             DefaultValue(typeof(double), @"C://Autotest//Logs"),
            ]
            public string LogDirectory
            {
                get { return _logDirectory; }
                set { _logDirectory = value; }
            }

            private int _receiveTime = 60000;
            [Category("B.���Բ���"),
             DisplayName("B01.����ʱ�� ms"),
             Description("����ʱ�� ����"),
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
            // ����ֵʾ��
            private ValueLimit _ais1CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.����֡����"),
             DisplayName("A01.AIS1"),
             Description("AIS1����֡����"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Ais1CountLimit
            {
                get { return _ais1CountLimit; }
                set { _ais1CountLimit = value; }
            }
            private ValueLimit _ais2CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.����֡����"),
             DisplayName("A01.AIS2"),
             Description("AIS2����֡����"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Ais2CountLimit
            {
                get { return _ais2CountLimit; }
                set { _ais2CountLimit = value; }
            }

            private ValueLimit _lais1CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.����֡����"),
             DisplayName("A01.LAIS1"),
             Description("LAIS1����֡����"),
             DefaultValue(typeof(ValueLimit), "[195,205] | int"),
            ]
            public ValueLimit Lais1CountLimit
            {
                get { return _lais1CountLimit; }
                set { _lais1CountLimit = value; }
            }

            private ValueLimit _lais2CountLimit = new ValueLimit("[195,205] | int");
            [Category("A.����֡����"),
             DisplayName("A01.LAIS2"),
             Description("LAIS2����֡����"),
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
            // �ڴ˴���Ӳ���ǰ��׼������
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            string wxbh = string.Format("���ǣ�");
            try
            {
                wxbh = wxbh + UserRunTimeVarPool["����"].ToString();
                LogInfo(LogInfoType.Error, string.Format("��ʼ {} ����������", wxbh));
            }
            catch (Exception)
            {
                wxbh = wxbh + "��ȡ���Ǳ��ʧ��";
                LogInfo(LogInfoType.Error, wxbh);
                return ExecOkError.Error;
            }

            filename = Path.Combine(_parameterSetting.LogDirectory, "AIS_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+".dat");
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                // ����dat�ļ�
            }
            try
            {
                //��ʼ��������
                StartReceive(false,_parameterSetting.IpAddr,_parameterSetting.IpPort);
                //����Ӧ��ȥң�����Ƿ����������ڱ仯������������ʱ���档
                Thread.Sleep(_parameterSetting.ReceiveTime);
                //�رռ�������
                StartReceive(true, _parameterSetting.IpAddr, _parameterSetting.IpPort);
                //��������ͨ������pc�Ĺ���
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ExecOkError.Error;
            }

            // ��������
            aisDataCheck(filename);

            LogResult($"{wxbh}- AIS1 ������֡��", Ais1Count, Ais1Count.ToString(), "֡", _limitSetting.Ais1CountLimit);
            LogResult($"{wxbh}- AIS2 ������֡��", Ais2Count, Ais2Count.ToString(), "֡", _limitSetting.Ais2CountLimit);
            LogResult($"{wxbh}- LAIS1 ������֡��", Lais1Count, Lais1Count.ToString(), "֡", _limitSetting.Lais1CountLimit);
            LogResult($"{wxbh}- LAIS2 ������֡��", Lais2Count, Lais2Count.ToString(), "֡", _limitSetting.Lais2CountLimit);

            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            // �ڴ˴���Ӳ��Ժ��������
            
            return ExecOkError.OK;
        }
        #endregion

        private void aisDataCheck(string datafileName)
        {
            // ������ʽ������ƥ��֡��
            Regex frameRegex = new Regex(@"������(\d+)֡");

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
                        // ��ȡ֡��
                        Ais1Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("AIS2"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // ��ȡ֡��
                        Ais2Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("LAIS1"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // ��ȡ֡��
                        Lais1Count = int.Parse(match.Groups[1].Value);
                    }
                }
                if (result.Contains("LAIS2"))
                {
                    LogInfo(LogInfoType.Log, result);
                    Match match = frameRegex.Match(result);
                    if (match.Success)
                    {
                        // ��ȡ֡��
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
                LogInfo(LogInfoType.Warning, "����tcp��checkaisʧ�ܣ�����CheckAis.exe�����Ƿ��Ѿ���");
            }
            return true;
        }

        
        /// <summary>
        /// Start a udpClient to listen
        /// </summary>
        /// <param name="isMonitor">if true, monitor stop</param>
        private void StartReceive(bool isMonitor,string hostIp, int hostPort)
        {
            if (!isMonitor) // δ�������������ʼ����
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(hostIp, hostPort);

                isReceiving = true;
                thrRecv = new Thread(ReceiveMessage)
                {
                    IsBackground = true
                };
                thrRecv.Start();
                LogInfo(LogInfoType.Normal, "tcp������������,��ʼ���������豸ת��������");
            }
            else // ���ڼ������������ֹ����
            {
                isReceiving=false;
                //thrRecv?.Join(); // �ȴ��߳̽�����ȷ���̰߳�ȫ�˳�
                thrRecv.Abort();//�����ȹر�����̣߳�������쳣
                tcpClient.Close();
                LogInfo(LogInfoType.Normal, "tcp�������ѹرգ�ֹͣ���������豸");
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
                // ʹ�� FileStream ��׷��ģʽ���ļ�����������д��
                using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length); // �����յ�������д���ļ�
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