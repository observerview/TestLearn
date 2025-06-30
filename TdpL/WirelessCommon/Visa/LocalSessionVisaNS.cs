using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.VisaNS;

namespace WirelessCommon.Visa
{
    public class LocalSessionVisaNS: IDisposable, ISession
    {
        private MessageBasedSession _vi;
        private string _errString;
        private string _visaErrString;
        public void Dispose()
        {
            _vi.Dispose();
        }

        public bool Intialize(string source)
        {
            try
            {
                this._vi = (MessageBasedSession)ResourceManager.GetLocalManager().Open(source);
            }
            catch (Exception exception)
            {
                this._errString = string.Format("VISA open operation failed ({0}).", source);
                this._visaErrString = string.Format("{0}: {1}", source, exception.Message);
                this._vi = null;
                return false;
            }
            return true;
        }

        public string ResourceName
        {
            get { return _vi.ResourceName; }
        }

        public int DefaultBufferSize
        {
            get { return _vi.DefaultBufferSize; }
            set { _vi.DefaultBufferSize = value; }
        }

        public int AsrlBaud
        {
            get
            {
                if (_vi is SerialSession)
                {
                    return (_vi as SerialSession).BaudRate;
                }
                return 0;
            }
            set
            {
                if (_vi is SerialSession)
                {
                    (_vi as SerialSession).BaudRate = value;
                }
            }
        }
        public int Timeout
        {
            get { return _vi.Timeout; }
            set { _vi.Timeout = value; }
        }

        public bool SendEndEnabled
        {
            get { return _vi.SendEndEnabled; }
            set { _vi.SendEndEnabled = value; }
        }

        public byte[] ReadByteArray()
        {
            return _vi.ReadByteArray();
        }

        public string ReadString()
        {
            return _vi.ReadString();
        }

        public void Write(byte[] cmd)
        {
            _vi.Write(cmd);
        }

        public void Write(string cmd)
        {
            _vi.Write(cmd);
        }

        public void Lock()
        {
        }

        public void Unlock()
        {
        }

        public string ErrString
        {
            get { return _errString; }
        }

        public string VisaErrString
        {
            get { return _visaErrString; }
        }
        public bool Lock(int timeoutMs)
        {
            return true;
        }
        public void ClearBuffer()
        {
            if (_vi is SerialSession)
            {
                (_vi as SerialSession).Clear();
            }
        }
    }
}
