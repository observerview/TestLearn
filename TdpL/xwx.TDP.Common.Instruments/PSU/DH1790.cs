using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WirelessCommon.Socket;
using WirelessCommon.Visa;

namespace xwx.TDP.Common.Instruments.PSU
{
    [DisplayName("DH1790"),
    FirmwareVersion("N/A"),
    Description("DH1790 系列 - 宽范围可编程直流电源")]
    public class DH1790 : PowerSupplyUnit
    {
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private int _cmdTimeInterval = 500;

        public int CmdTimeInterval { get { return _cmdTimeInterval; } set { _cmdTimeInterval = value; } }


        // 发送指令
        public bool SendCommand(string command)
        {
            try
            {
                if (!_tcpClient.Connected)
                {
                    errString = string.Format("Not connected to the instrument.");
                    return false;
                }

                byte[] data = System.Text.Encoding.ASCII.GetBytes(command + "\n");
                _stream.Write(data, 0, data.Length);
                return true;
            }
            catch (IOException e)
            {
                errString = string.Format($"IOException: {e}");
                return false;
            }
        }

        // 从仪器接收数据
        public string ReceiveData()
        {
            try
            {
                if (!_tcpClient.Connected)
                {
                    errString = string.Format("Not connected to the instrument.");
                    return null;
                }

                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
            }
            catch (IOException e)
            {
                errString = string.Format($"IOException: {e}");
                return null;
            }
        }

        private uint _numberOfPath = 1;
        private double[] _maxVoltageV = new double[] { 80 };

        public override uint NumberOfPath
        {
            get
            {
                return _numberOfPath;
            }
            set
            {
                _numberOfPath = value;
                _maxVoltageV = new double[value];
                for (int i = 0; i < _maxVoltageV.Length; i++)
                {
                    _maxVoltageV[i] = 80;
                }
            }
        }
        public override double[] MaxVoltageV
        {
            get
            {
                return _maxVoltageV;
            }
            set
            {
                if (value.Length != _numberOfPath)
                {
                    throw new Exception(string.Format("Only {0} voltage path supported", _numberOfPath));
                }
                else
                {
                    _maxVoltageV = value;
                }
            }
        }

        public override bool Initialize(string resource)
        {
            return Initialize(resource,"");
        }
        public override bool Initialize(string resource, string remoteServer = "")
        {
            string devip = resource.Substring(0, resource.IndexOf(':'));
            int devport = int.Parse(resource.Substring(resource.IndexOf(":")));
            try
            {
                if (remoteServer == "")
                {
                    _tcpClient = new TcpClient(devip,devport);
                    _stream = _tcpClient.GetStream();
                }
                else
                {
                    errString = string.Format("RemoteServer is not support now.", remoteServer);
                }
                bool ret = _tcpClient.Connected;
                return ret;
            }
            catch (Exception ex)
            {
                errString = string.Format("Socket connet failed ({0}).", resource);
                return false;
            }
        }

        public new bool QueryString(string cmd , out string result)
        {
            try
            {
                SendCommand("cmd");
                Thread.Sleep(_cmdTimeInterval);
                result = ReceiveData();
                return true;
            }
            catch (Exception ex)
            {
                errString = ex.Message;
                result = null;
                return false;
            }
        }
        public new bool QueryDouble(string cmd , out double result)
        {
            try
            {
                SendCommand("cmd");
                Thread.Sleep(_cmdTimeInterval);
                var stringResult = ReceiveData();
                double.TryParse(stringResult, out result);
                return true;
            }
            catch (Exception ex)
            {
                errString = ex.Message;
                result = double.NaN;
                return false;
            }
        }
        public new bool WriteString(string cmd)
        {
            try
            {
                SendCommand("*IDN?");
                Thread.Sleep(_cmdTimeInterval);
                return true;
            }
            catch (Exception ex)
            {
                errString = ex.Message;
                return false;
            }
        }
        public override bool GetIdn(out string idn)
        {
            return QueryString("*IDN?", out idn);
           
        }
        public override bool Reset()
        {
            return WriteString("*RST");
           
        }
        public override bool SetVoltage(uint pathNo, double voltageV)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return WriteString(string.Format("VOLT {0:f}", voltageV));
        }
        public override bool GetVoltage(uint pathNo, out double voltageV)
        {
            voltageV = 0;
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return QueryDouble("MEAS:VOLT?", out voltageV);
        }
        public override bool SetCurrentLimit(uint pathNo, double currentA)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return WriteString(string.Format("CURR:PROT {0:f}", currentA));
        }
        public override bool GetCurrent(uint pathNo, out double currentA)
        {
            currentA = 0;
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return QueryDouble("MEAS:CURR?", out currentA);
        }
        public override bool SetOutputStatus(uint pathNo, bool status)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }

            if (status)
            {
                return WriteString("OUTP ON");

            }
            else
            {
                return WriteString("OUTP OFF");
            }

        }


    }


}
