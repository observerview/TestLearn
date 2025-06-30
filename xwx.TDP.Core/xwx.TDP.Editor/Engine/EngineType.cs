using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Editor.Engine
{
    internal enum EngineType
    {
        [EnumDisplayName("Globe")]
        Globe,
		[EnumDisplayName("Case Specified")]
        CaseSpecified,
		[EnumDisplayName("Skip")]
        Skip
    }
}
