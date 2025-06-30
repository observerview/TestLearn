using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Editor.Engine
{
    internal enum EngineMode_OK
    {
        [EnumDisplayName("Ignore Failues")]
        [Description("If the case failed, ignore it and continue. This is the normal mode.")]
        IgnoreFail,
        [EnumDisplayName("Abort On Failues")]
        [Description("If the case failed, abort the engine.")]
        AbortOnFail,
        [EnumDisplayName("Retry On Failues")]
        [Description("If the case failed, retest the case for %N% times.")]
        RetestOnFail,
        [EnumDisplayName("Retest")]
        [Description("Repeat the case, until %N% test totally.")]
        Repeat,
        [Description("Repeat the case, until %N% failues totally.")]
        [EnumDisplayName("Retest Until Failues")]
        RepeatUntilFail,
        [Description("Repeat the case, until %N% pass totally.")]
        [EnumDisplayName("Retest Until Pass")]
        RepeatUntilPass
    }
}
