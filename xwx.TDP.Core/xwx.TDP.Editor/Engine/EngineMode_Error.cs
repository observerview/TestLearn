using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Editor.Engine
{
    internal enum EngineMode_Error
    {
        [Description("When error occured, abort the engine. This is the normal mode.")]
        [EnumDisplayName("Abort")]
        Abort,
        [Description("When error occured, ignore it and continue.")]
        [EnumDisplayName("Ignore")]
        Ignore,
        [Description("When error occured, retry the case for %N% times, then abort.")]
        [EnumDisplayName("Retry Then Abort")]
        RetryThenAbort,
        [EnumDisplayName("Retry Then Continue")]
        [Description("When error occured, retry the case for %N% times, then continue.")]
        RetryThenContinue
    }
}
