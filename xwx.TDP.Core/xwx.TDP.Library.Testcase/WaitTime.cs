using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.BaseCase
{
    
    [Category("Flow Control")]
    [DisplayName("Wait Time")]
    [DisplayColor("Green")]
    [Description("Wait for a specified moment.")]
    public class WaitTime : CoreCase
    {
        private class ParameterSetting : ConfigBase
        {
            private int _delayTimeSecond = 10;

            
            [Category("Setting")]
            [DisplayName("Delay Time (s)")]
            [DefaultValue(10)]
            [ValueLimits(new string[] { "[1,3600000]" })]
            [Description("Set the delay time.")]
            public int DelayTimeSecond
            {
                get
                {
                    return _delayTimeSecond;
                }
                set
                {
                    _delayTimeSecond = value;
                }
            }
        }
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get
            {
                return (ConfigBase)(object)_parameterSetting;
            }
            set
            {
                if (value is ParameterSetting)
                {
                    _parameterSetting = value as ParameterSetting;
                }
            }
        }
        public override ExecOkError Exec()
        {
            LogInfo(LogInfoType.Log,"wait time Start......");
            for (int i = 0; i < _parameterSetting.DelayTimeSecond; i++)
            {
                LogTip((LogTipType)2, string.Format("Waiting ({0} s)...", _parameterSetting.DelayTimeSecond - i));
                Thread.Sleep(1000);
            }
            LogTip((LogTipType)1, string.Empty);
            LogInfo(LogInfoType.Log, "wait time Ended......");
            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }
    }
}
