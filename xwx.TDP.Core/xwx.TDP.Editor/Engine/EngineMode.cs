using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor.Engine
{
    internal class EngineMode
    {
        private EngineMode_Error _error;

        private EngineMode_OK _oK;

        private uint _errorRetry = 3;

        private uint _okRetry = 3;

        private EngineType _type;
        public EngineType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
        public EngineMode_Error Error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
        public EngineMode_OK OK
        {
            get
            {
                return this._oK;
            }
            set
            {
                this._oK = value;
            }
        }
        public uint ErrorRetry
        {
            get
            {
                return this._errorRetry;
            }
            set
            {
                this._errorRetry = value;
            }
        }
        public uint OkRetry
        {
            get
            {
                return this._okRetry;
            }
            set
            {
                this._okRetry = value;
            }
        }

        
    }
}
