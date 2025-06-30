using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.BaseCase
{
    [Category("Demo Case")]
    [DisplayName("Random OK or Error")]
    [Description("Demo Case, result is pass or fail")]
    public class DemoRandomOkError : CoreCase
    {
        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            bool flag = random.NextDouble() > 0.5;
            LogResult(LogResultType.Normal, "Random Pass/Fail", flag, flag.ToString(), string.Empty, new ValueLimitCollection("{True} | bool"));
            if (flag)
            {
                return ExecOkError.OK;
            }
            return ExecOkError.Error;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }
    }
}
