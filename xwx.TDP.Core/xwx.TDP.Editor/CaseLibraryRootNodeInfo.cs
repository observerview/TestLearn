using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;

namespace xwx.TDP.Editor
{
    internal class CaseLibraryRootNodeInfo
    {
        public IAppHooker AppHooker
        {
            get
            {
                return this._appHooker;
            }
            set
            {
                this._appHooker = value;
            }
        }

        public Assembly ApplicationAssembly
        {
            get
            {
                return this._applicationAssembly;
            }
            set
            {
                this._applicationAssembly = value;
            }
        }
 
        public CaseLibraryRootNodeInfo(Assembly applicationAssembly, IAppHooker appHooker)
        {
            this._applicationAssembly = applicationAssembly;
            this._appHooker = appHooker;
        }

        private IAppHooker _appHooker;

        private Assembly _applicationAssembly;
    }
}
