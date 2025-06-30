using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor.Engine
{
    internal class TestCaseInfo
    {
        private string _assemblyName = string.Empty;

        private Version _assemblyVersion;

        private string _testCaseFullName = string.Empty;

        private EngineMode _caseEngineMode = new EngineMode();

        private NameValueCollection _limits = new NameValueCollection();

        private NameValueCollection _parameters = new NameValueCollection();
        public string AssemblyName
        {
            get
            {
                return this._assemblyName;
            }
            set
            {
                this._assemblyName = value;
            }
        }
        public Version AssemblyVersion
        {
            get
            {
                return this._assemblyVersion;
            }
            set
            {
                this._assemblyVersion = value;
            }
        }

        public string TestCaseFullName
        {
            get
            {
                return this._testCaseFullName;
            }
            set
            {
                this._testCaseFullName = value;
            }
        }

        public EngineMode CaseEngineMode
        {
            get
            {
                return this._caseEngineMode;
            }
            set
            {
                this._caseEngineMode = value;
            }
        }
        public NameValueCollection Limits
        {
            get
            {
                return this._limits;
            }
            set
            {
                this._limits = value;
            }
        }

        public NameValueCollection Parameters
        {
            get
            {
                return this._parameters;
            }
            set
            {
                this._parameters = value;
            }
        }

        
    }
}
