using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor.Engine
{
    internal class TdpTestSequenceInfo
    {
        private string _author = string.Empty;
        private string _displayName = string.Empty;
        private string _description = string.Empty;
        private DateTime _createdTime = DateTime.MinValue;
        private DateTime _modifiedTime = DateTime.MinValue;
        private EngineMode _globeEngineMode = new EngineMode();
        private uint _executTimes = 1U;
        private string _xmlFileFullName = string.Empty;
        public string Author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
            }
        }
        public string DisplayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
            }
        }
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        public DateTime CreatedTime
        {
            get
            {
                return this._createdTime;
            }
            set
            {
                this._createdTime = value;
            }
        }
        public DateTime ModifiedTime
        {
            get
            {
                return this._modifiedTime;
            }
            set
            {
                this._modifiedTime = value;
            }
        }
        public EngineMode GlobeEngineMode
        {
            get
            {
                return this._globeEngineMode;
            }
            set
            {
                this._globeEngineMode = value;
            }
        }
        public uint ExecutTimes
        {
            get
            {
                return this._executTimes;
            }
            set
            {
                this._executTimes = ((value < 1U) ? 1U : value);
            }
        }
        public string XmlFileFullName
        {
            get
            {
                return this._xmlFileFullName;
            }
            set
            {
                this._xmlFileFullName = value;
            }
        }
    }
}
