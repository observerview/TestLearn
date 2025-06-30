using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor.LogViews
{
    internal interface IInternalLogView
    {
        bool CanBeDisabled { get; }
        bool IsAdvancedView { get; }
    }
}
