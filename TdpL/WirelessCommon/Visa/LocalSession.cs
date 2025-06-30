using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Visa;
using Ivi.Visa;

namespace WirelessCommon.Visa
{
    public class LocalSession : IDisposable, ISession
    {
        private MessageBasedSession _vi;
        private string _errString;
        private string _visaErrString;
        private int _timeoutMilliseconds;
        private int _defaultBufferSize = 256;
        public void Dispose()
        {
            _vi.Dispose();
        }

        public bool Intialize(string source)
        {
            using (var rmSession = new ResourceManager())
            {
                try
                {
                    this._vi = (MessageBasedSession)rmSession.Open(source,AccessModes.None, _timeoutMilliseconds);


                }
                catch (VisaException exception)
                {
                    this._errString = string.Format("VISA open operation failed ({0}).", source);
                    this._visaErrString = string.Format("{0}: {1}", source, exception.Message);
                    this._vi = null;
                    return false;
                }
            }

            return true;
        }
        public string ResourceName
        {
            get { return _vi.ResourceName; }
        }

        /// <summary>
        /// useless, read() function can read to end 
        /// </summary>
        public int DefaultBufferSize
        {
            get { return _defaultBufferSize; }
            set { _defaultBufferSize = value; }
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
            get { return _timeoutMilliseconds; }
            set { _timeoutMilliseconds = value; }
        }

        public bool SendEndEnabled
        {
            get { return _vi.SendEndEnabled; }
            set { _vi.SendEndEnabled = value; }
        }

        public byte[] ReadByteArray()
        {
            return _vi.RawIO.Read();
        }

        public string ReadString()
        {
            return _vi.RawIO.ReadString();
        }

        public void Write(byte[] cmd)
        {
            _vi.RawIO.Write(cmd);
        }

        public void Write(string cmd)
        {
            _vi.RawIO.Write(cmd);
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