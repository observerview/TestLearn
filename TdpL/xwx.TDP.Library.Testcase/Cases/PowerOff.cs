using System;
using System.ComponentModel;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.Common.Cases
{
    [Category("封装的仪表控制"),
     DisplayName("电源关闭"),
     Description("仅关闭电源"),
    ]
    public class PowerOff : CoreCaseCommon
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

            if (!_psu.SetOutputStatus(_parameterSetting.PathNumber, false))
            {
                LogMessage(LogMessageType.Error, "关闭电源失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogMessage(LogMessageType.Notify, "电源已关闭");
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