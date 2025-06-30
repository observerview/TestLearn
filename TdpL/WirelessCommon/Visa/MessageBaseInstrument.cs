using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WirelessCommon.Visa
{
    public class MessageBaseInstrument
    {
        private int _queryTimeInterval = 500;
        private int _timeOutOperation = 0x2bf20;
        protected string errString = string.Empty;
        protected ISession vi;
        private string visaErrString = string.Empty;
        private const string WaitInstruction = "!####!";

        public event MessageReadWriteEventHandler MessageReadWrite;
        public void Lock()
        {
            try
            {
                vi?.Lock();
            }
            catch (Exception exception)
            {
                this.errString = "VISA writing string operation failed.";
                this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                return;
            }
        }
        public void Unlock()
        {
            try
            {
                vi?.Unlock();
            }
            catch (Exception exception)
            {
                this.errString = "VISA writing string operation failed.";
                this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                return;
            }
        }
        public bool Lock(int milliscond)
        {
            try
            {
                if (vi != null)
                    return vi.Lock(milliscond);
            }
            catch (Exception exception)
            {
                this.errString = "VISA writing string operation failed.";
                this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                return false;
            }
            return true;
        }
        protected string GetWaitString(int ms)
        {
            return ("!####!" + ms.ToString());
        }
        public virtual bool Initialize(string resource, string remoteServer)
        {
            try
            {
                if (remoteServer == "")
                {
                    this.vi = new LocalSession();
                }
                else
                {
                    //this.vi = new RemoteSession(remoteServer);//没空做，留坑后面再说
                    this.errString = string.Format("RemoteServer is not support now.", remoteServer);
                }
                bool ret = this.vi.Intialize(resource);
                this.vi.Timeout = 30000;
                this.errString = this.vi.ErrString;
                this.visaErrString = this.vi.VisaErrString;
                return ret;
            }
            catch (Exception exception)
            {
                this.errString = string.Format("VISA open operation failed ({0}).", resource);
                this.visaErrString = string.Format("{0}", exception.Message);
                this.vi = null;
                return false;
            }
        }
        public virtual bool Initialize(string resource)
        {
            return Initialize(resource, "");
        }

        public bool QueryBoolean(string cmd, out bool result)
        {
            return this.QueryBoolean(cmd, 0, out result);
        }

        public bool QueryBoolean(string cmd, int delayMs, out bool result)
        {
            result = false;
            return (this.WriteString(cmd) && this.ReadBoolean(delayMs, out result));
        }

        public bool QueryBytes(string cmd, out byte[] data)
        {
            return this.QueryBytes(cmd, 0, out data);
        }

        public bool QueryBytes(string cmd, int delayMs, out byte[] data)
        {
            data = new byte[0];
            return (this.WriteString(cmd) && this.ReadBytes(delayMs, out data));
        }

        public bool QueryDouble(string cmd, out double result)
        {
            return this.QueryDouble(cmd, 0, out result);
        }

        public bool QueryDouble(string cmd, int delayMs, out double result)
        {
            result = double.NaN;
            return (this.WriteString(cmd) && this.ReadDouble(delayMs, out result));
        }

        public bool QueryInteger(string cmd, out int result)
        {
            return this.QueryInteger(cmd, 0, out result);
        }

        public bool QueryInteger(string cmd, int delayMs, out int result)
        {
            result = -2147483648;
            return (this.WriteString(cmd) && this.ReadInteger(delayMs, out result));
        }

        public bool QueryString(string cmd, out string result)
        {
            return this.QueryString(cmd, 0, out result);
        }

        public bool QueryString(string cmd, int delayMs, out string result)
        {
            result = string.Empty;
            return (this.WriteString(cmd) && this.ReadString(delayMs, out result));
        }

        public bool ReadBoolean(out bool result)
        {
            return this.ReadBoolean(0, out result);
        }

        public bool ReadBoolean(int delayMs, out bool result)
        {
            string str = string.Empty;
            result = false;
            if (this.ReadString(delayMs, out str))
            {
                if ((string.Compare(str, "1", true) == 0) || (string.Compare(str, "TRUE", true) == 0))
                {
                    result = true;
                    return true;
                }
                if ((string.Compare(str, "0", true) == 0) || (string.Compare(str, "FALSE", true) == 0))
                {
                    result = false;
                    return true;
                }
                this.errString = str;
            }
            return false;
        }

        public bool ReadBytes(out byte[] data)
        {
            return this.ReadBytes(0, out data);
        }

        public bool ReadBytes(int delayMs, out byte[] data)
        {
            data = new byte[0];
            try
            {
                Thread.Sleep(delayMs);
                data = this.vi.ReadByteArray();
                if (this.MessageReadWrite != null)
                {
                    this.MessageReadWrite(this, new MessageReadWriteEventArgs(MessageReadWriteDirection.Read, data));
                }
                return true;
            }
            catch (Exception exception)
            {
                this.errString = "VISA reading bytes operation failed.";
                this.visaErrString = string.Format("{0}: {1}", "ReadBytes", exception.Message);
                return false;
            }
        }

        public bool ReadDouble(out double result)
        {
            return this.ReadDouble(0, out result);
        }

        public bool ReadDouble(int delayMs, out double result)
        {
            string str = string.Empty;
            result = double.NaN;
            return (this.ReadString(delayMs, out str) && double.TryParse(str, out result));
        }

        public bool ReadInteger(out int result)
        {
            return this.ReadInteger(0, out result);
        }

        public bool ReadInteger(int delayMs, out int result)
        {
            string str = string.Empty;
            result = -2147483648;
            return (this.ReadString(delayMs, out str) && int.TryParse(str, out result));
        }

        public bool ReadString(out string result)
        {
            return this.ReadString(0, out result);
        }

        public bool ReadString(int delayMs, out string result)
        {
            result = string.Empty;
            try
            {
                Thread.Sleep(delayMs);
                result = this.vi.ReadString().Trim();
                if (this.MessageReadWrite != null)
                {
                    this.MessageReadWrite(this, new MessageReadWriteEventArgs(MessageReadWriteDirection.Read, result));
                }
                return true;
            }
            catch (Exception exception)
            {
                this.errString = "VISA reading string operation failed.";
                this.visaErrString = string.Format("{0}: {1}", "ReadString", exception.Message);
                return false;
            }
        }

        public bool WriteBytes(byte[] data)
        {
            return this.WriteBytes(data, 0);
        }

        public bool WriteBytes(byte[] data, int delayMs)
        {
            try
            {
                if (this.MessageReadWrite != null)
                {
                    this.MessageReadWrite(this, new MessageReadWriteEventArgs(MessageReadWriteDirection.Write, data));
                }
                this.vi.Write(data);
                Thread.Sleep(delayMs);
                return true;
            }
            catch (Exception exception)
            {
                this.errString = "VISA writing bytes operation failed.";
                this.visaErrString = string.Format("[Byte Array]: {1}", data, exception.Message);
                return false;
            }
        }

        public bool WriteString(List<string> cmds)
        {
            foreach (string str in cmds)
            {
                if (!this.WriteString(str))
                {
                    return false;
                }
            }
            return true;
        }

        public bool WriteString(string cmd)
        {
            return this.WriteString(cmd, 0);
        }

        public bool WriteString(string cmd, int delayMs)
        {
            if (cmd.StartsWith("!####!"))
            {
                Thread.Sleep(int.Parse(cmd.Substring("!####!".Length)));
                return true;
            }
            try
            {
                if (this.MessageReadWrite != null)
                {
                    this.MessageReadWrite(this, new MessageReadWriteEventArgs(MessageReadWriteDirection.Write, cmd));
                }
                this.vi.Write(cmd);
                Thread.Sleep(delayMs);
                return true;
            }
            catch (Exception exception)
            {
                this.errString = "VISA writing string operation failed.";
                this.visaErrString = string.Format("{0}: {1}", cmd, exception.Message);
                return false;
            }
        }

        public virtual int DefaultBufferSize
        {
            get
            {
                try
                {
                    return this.vi.DefaultBufferSize;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.vi.DefaultBufferSize = value;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Get DefaultBufferSize", exception.Message);
                    return;
                }
            }
        }

        public int QueryTimeInterval
        {
            get
            {
                try
                {
                    return this._queryTimeInterval;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return 0;
                }
            }
            set
            {
                try
                {
                    this._queryTimeInterval = value;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return;
                }
            }
        }

        public virtual string ResourceName
        {
            get
            {
                try
                {
                    return this.vi.ResourceName;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return "";
                }
            }
        }

        public int TimeOutOperation
        {
            get
            {
                try
                {
                    return this._timeOutOperation;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return 0;
                }
            }
            set
            {
                try
                {
                    this._timeOutOperation = value;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return;
                }
            }
        }

        public virtual int TimeOutSession
        {
            get
            {
                try
                {
                    return this.vi.Timeout;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.vi.Timeout = value;
                }
                catch (Exception exception)
                {
                    this.errString = "VISA writing string operation failed.";
                    this.visaErrString = string.Format("{0}: {1}", "Set DefaultBufferSize", exception.Message);
                    return;
                }
            }
        }

        public string VisaErrString
        {
            get
            {
                return this.visaErrString;
            }
        }
        public string ErrString
        {
            get
            {
                return this.errString;
            }
        }

        public delegate void MessageReadWriteEventHandler(object sender, MessageReadWriteEventArgs args);
    }
}
