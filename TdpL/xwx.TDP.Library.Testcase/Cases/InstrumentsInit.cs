using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using WirelessCommon.Visa;
using xwx.TDP.Common.Instruments;

namespace xwx.TDP.Library.Common.Cases
{
    [Category("封装的仪表控制"),
     DisplayName("通用.仪表初始化"),
     DisplayColor("red"),
     Description("对于有仪表使用commonn.Intruments库的测试站，必选"),
    ]
    public class InstrumentsInit : CoreCaseCommon
    {
        #region Parameter Define
        private class ParameterSetting : ConfigBase
        {

        }
        #endregion Parameter Define

        #region Parameter Geter and Setter
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get { return this._parameterSetting; }
            set
            {
                if (value != null && value is ParameterSetting)
                {
                    this._parameterSetting = value as ParameterSetting;
                }
            }
        }
        #endregion Parameter Geter and Setter


        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            var instr = new MessageInstruments();

            #region Power Supply
            if (!Settings.Instance.PsuDriver.Initialize(out instr))
            {
                LogMessage(LogMessageType.Warning, instr.ErrString);
                LogInfo(LogInfoType.Error, "Instruments Init Error");
                return ExecOkError.Error;
            }
            //instr.IsOutputVisaCommandToConsole = Configurations.Instance.ShowInstrumentsLog;
            LogResult(Settings.Instance.PsuDriver.DevType, Settings.Instance.PsuDriver.DevAddr, "");
            UserRunTimeVarPool[CommonRuntimeVar.Instrument_PSU] = instr;

            #endregion Power Supply


            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }


    }
}
