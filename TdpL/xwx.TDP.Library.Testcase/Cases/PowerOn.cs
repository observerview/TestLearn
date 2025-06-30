using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.Common.Cases
{
    [Category("封装的仪表控制"),
     DisplayName("电源上电"),
     Description("仅上电"),
    ]
    public class PowerOn : CoreCaseCommon
    {
        private string _idn = "";
        //private double _current;
        private class ParameterSetting : ConfigBase
        {
            private uint _pathNumber = 0;
            [Category("A.程控电源设置"),
             DisplayName("A01.程控电源通道"),
             Description("设置对应的程控电源通道,对于只有一个通道的电源，设0；"),
             DefaultValue(typeof(uint), "0"),
            ]
            public uint PathNumber
            {
                get { return _pathNumber; }
                set { _pathNumber = value; }
            }

            private double _psuVoltage = 18;
            [Category("A.程控电源设置"),
             DisplayName("A02.输出电压(V)"),
             Description("设置程控电源输出电压"),
             DefaultValue(typeof(double), "18")
            ]
            public double PsuVoltage
            {
                get { return _psuVoltage; }
                set { _psuVoltage = value; }
            }

            private double _psuCurrentLimit = 1.5;
            [Category("A.程控电源设置"),
             DisplayName("A03.输出电流上限(A)"),
             Description("设置程控电源输出电流上限"),
             DefaultValue(typeof(double), "1.5")
            ]
            public double PsuCurrentLimit
            {
                get { return _psuCurrentLimit; }
                set { _psuCurrentLimit = value; }
            }

        }

        #region Parameter Getter and Setter
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get
            {
                return _parameterSetting;
            }
            set
            {
                if (value != null && value is ParameterSetting)
                {
                    _parameterSetting = value as ParameterSetting;
                }
            }
        }
        #endregion Parameter Getter and Setter


        public override ExecOkError Exec()
        {
            if (!_psu.GetIdn(out _idn))
            {
                LogMessage(LogMessageType.Error, "获取电源信息失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogMessage(LogMessageType.Normal, "电源ID: " + _idn);
            }
            if (!_psu.SetCurrentLimit(_parameterSetting.PathNumber, _parameterSetting.PsuCurrentLimit))
            {
                LogMessage(LogMessageType.Error, "设置电源输出电流上限失败!");
                return ExecOkError.Error;
            }

            if (!_psu.SetVoltage(_parameterSetting.PathNumber, _parameterSetting.PsuVoltage))
            {
                LogMessage(LogMessageType.Error, "设置电源输出电压失败!");
                return ExecOkError.Error;
            }

            if (!_psu.SetOutputStatus(_parameterSetting.PathNumber, true))
            {
                LogMessage(LogMessageType.Error, "打开电源失败!");
                return ExecOkError.Error;
            }

            else
            {
                LogMessage(LogMessageType.Notify, "电源已打开");

            }


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
