using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Extern.Interface
{
    public interface IPlanExecutor
    {
        void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter);

        void LogMessage(LogMessageType logMessageType, string text);
    }
}
