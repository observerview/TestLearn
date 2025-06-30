using System;
using System.Windows.Forms;

namespace TestManager.Extern
{
    public interface IAppHooker
    {
        Form OnwerForm { set; }

        bool IsTestManagerApp { get; }

        bool IsDebugMode { get; }

        uint MaxExecutorNum { get; }

        ToolStripItem[] ToolStripItems { get; }

        string[] TestResultTitles { get; }

        Type[] UserDefinedLogViewsType { get; }

        string DefaultTestResultDirectory { get; set; }
    }
}
