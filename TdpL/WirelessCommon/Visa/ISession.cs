using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WirelessCommon.Visa
{
    public interface ISession
    {
        bool Intialize(string source);
        string ResourceName { get; }
        int DefaultBufferSize { get; set; }
        int Timeout { get; set; }
        bool SendEndEnabled { get; set; }
        int AsrlBaud { get; set; }
        byte[] ReadByteArray();
        string ReadString();
        void Write(byte[] cmd);
        void Write(string cmd);
        void Lock();
        void Unlock();
        bool Lock(int timeoutMs);
        string ErrString { get; }
        string VisaErrString { get; }
        void ClearBuffer();
    }
}
