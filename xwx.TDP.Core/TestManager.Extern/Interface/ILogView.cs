using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Extern.Interface
{
    public interface ILogView
    {
        void LogResult(LogResultType logResultType, string title, object result, string resultString, string unit, object limit);

        void LogInfo(LogInfoType logInfoType, string title, string value, string unit);

        void LogTip(LogTipType logTipType, string text, float fontSize, bool isAlignCenter);

        void LogMessage(LogMessageType logMessageType, string text);

        void LogUserDefinedData(ILogData data, out List<object> retValues);
    }
}
