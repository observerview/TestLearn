using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Editor.Misc
{
    public enum TdpEdition
    {
        // Token: 0x0400000E RID: 14
        [EnumDisplayName("Super Lite")]
        SuperLite = 1,
        // Token: 0x0400000F RID: 15
        [EnumDisplayName("Lite")]
        Lite,
        // Token: 0x04000010 RID: 16
        [EnumDisplayName("Stardard")]
        Standard,
        // Token: 0x04000011 RID: 17
        [EnumDisplayName("Premium")]
        Premium
    }
}
