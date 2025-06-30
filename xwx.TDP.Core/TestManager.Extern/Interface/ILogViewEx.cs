using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestManager.Extern.Interface
{
    public interface ILogViewEx : ILogView
    {
        IRunTimeVarPoolReadOnly EngineRunTimeVarPool { set; }

        IRunTimeVarPoolReadOnly UserRunTimeVarPool { set; }

        bool IsDisposed { get; }

        bool Disposing { get; }

        string LogDirectory { set; }

        IPlanExecutor PlanExecutorInstance { set; }

        object MdiDockPanel { set; }

        string LogViewName { get; }

        Keys OpenViewShortcutKeys { get; }

        Font WindowsFont { set; }

        void ShowView();

        void HideView();

        void CloseView();

        void LogReset();

        void LogEngineStatus(LogEngineStatusType logEngineStatusType);

        void LogEngineTrace(LogEngineTraceType logEngineTraceType, string message);
    }
}
