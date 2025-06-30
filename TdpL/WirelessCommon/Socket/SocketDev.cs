using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WirelessCommon.Socket
{
    public class SocketDev
    {
        private TcpClient _client;
        public TcpClient Client {  get { return _client; } } 
        private NetworkStream _stream;
        private int _port;
        private string _devIp;

        public SocketDev(string devIp, int devPort)
        {
            _devIp = devIp;
            _port = devPort;
        }
        // 连接到仪器
        public bool Connect()
        {
            try
            {
                _client = new TcpClient(_devIp, _port);
                _stream = _client.GetStream();
                return _client.Connected;
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
                return false;
            }
        }
        // 断开连接
        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
        }

        // 发送命令到仪器
        public bool SendCommand(string command)
        {
            try
            {
                if (!_client.Connected)
                {
                    Console.WriteLine("Not connected to the instrument.");
                    return false;
                }

                byte[] data = System.Text.Encoding.ASCII.GetBytes(command + "\n");
                _stream.Write(data, 0, data.Length);
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException: {e}");
                return false;
            }
        }

        // 从仪器接收数据
        public string ReceiveData()
        {
            try
            {
                if (!_client.Connected)
                {
                    Console.WriteLine("Not connected to the instrument.");
                    return null;
                }

                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                return System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException: {e}");
                return null;
            }
        }

        // 检查连接状态
        public bool IsConnected()
        {
            return _client?.Connected ?? false;
        }
    }
}
