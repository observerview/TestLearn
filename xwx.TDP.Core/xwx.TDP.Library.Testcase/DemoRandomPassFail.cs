using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using TestManager.Extern;
using System.Threading;

namespace xwx.TDP.Library.BaseCase
{
    [Category("Demo Case")]
    [DisplayName("Random PASS or Fail")]
    [Description("Demo Case, result is pass or fail")]
    public class DemoRandomPassFail : CoreCase
    {
        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            Thread.Sleep(1000);
            Random random = new Random(DateTime.Now.Millisecond);
            bool flag = random.NextDouble() > 0.5;
            LogResult(LogResultType.Normal,
                "Random Pass/Fail",
                flag,
                flag.ToString(),
                string.Empty,
                new ValueLimitCollection("{True} | bool"));

            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }
    }
}
