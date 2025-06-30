using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Extern.Interface;
using xwx.TDP.Common.Instruments.PSU;

namespace xwx.TDP.Library.Common
{
    public class CommonRuntimeVar
    {
        //
        public const string Instrument_PSU = "Instrument PSU";

    }
    public abstract class CoreCaseCommon : CoreCase
    {
        protected PowerSupplyUnit _psu
        {
            get
            {
                if (UserRunTimeVarPool.ContainKey(CommonRuntimeVar.Instrument_PSU))
                {
                    return (PowerSupplyUnit)this.UserRunTimeVarPool[CommonRuntimeVar.Instrument_PSU];
                }
                LogInfo(LogInfoType.Error, "Power Supply 未初始化");
                return null;
            }
            set
            {
                UserRunTimeVarPool[CommonRuntimeVar.Instrument_PSU] = value;
            }
        }
        protected string _dutSN
        {
            get
            {
                if (UserRunTimeVarPool.ContainKey(EngineRunTimeVar.DutSerialsNumber))
                {
                    return (string)UserRunTimeVarPool[EngineRunTimeVar.DutSerialsNumber];
                }
                LogInfo(LogInfoType.Error, "DutSerialsNumber 未初始化");
                return null;
            }
            set
            {
                UserRunTimeVarPool[EngineRunTimeVar.DutSerialsNumber] = value;
            }
        }
       
    }
}
