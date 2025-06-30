using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;

namespace xwx.TDP.Library.Common.Cases
{
    [Category("封装的仪表控制"),
     DisplayName("电源上电并测量电流"),
     Description("电源上电并测量电流，可设置上电前后的等待时间，单位s"),
    ]
    public class PowerResetAndReadCurrent : CoreCaseCommon
    {
        private string _idn = "";
        private double _current;
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

            private double _delayTimeBeforePowerOn = 3.0;
            [Category("A.程控电源设置"),
            DisplayName("A04.上电前延时时间(s)"),
            Description("设置电源上电前的等待时间，单位：秒"), 
            DefaultValue(typeof(double), "3.0")
            ]
            public double DelayTimeBeforePowerOn
            {
                get { return _delayTimeBeforePowerOn; }
                set { _delayTimeBeforePowerOn = value; }
            }

            private double _delayTimeAfterPowerOn = 3.0;
            [Category("A.程控电源设置"),
            DisplayName("A05.电源上电后延时时间(s)"),
            Description("设置电源上电后的等待时间，单位：秒"),
            DefaultValue(typeof(double), "3.0")
            ]
            public double DelayTimeAfterPowerOn
            {
                get { return _delayTimeAfterPowerOn; }
                set { _delayTimeAfterPowerOn = value; }
            }

        }

        private class LimitSetting : ConfigBase
        {
            private ValueLimit _psuCurrent = new ValueLimit("[0,15] | double");
            [Category("A.电源电流"),
             DisplayName("A01.电源电流值"),
             Description("从程控电源读取的电流值"),
             DefaultValue(typeof(ValueLimit), "[0,15] | double"),
            ]
            public ValueLimit PsuCurrent
            {
                get { return _psuCurrent; }
                set { _psuCurrent = value; }
            }
        }

        #region Parameter & Limit Getter and Setter

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

        private LimitSetting _limitSetting = new LimitSetting();
        public override ConfigBase CaseLimitSetting
        {
            get
            {
                return this._limitSetting;
            }
            set
            {
                if (value != null && value is LimitSetting)
                {
                    this._limitSetting = value as LimitSetting;
                }
            }
        }
        #endregion


        public override ExecOkError Exec()
        {
            if (!_psu.GetIdn(out _idn))
            {
                LogInfo(LogInfoType.Error, "获取电源信息失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogInfo(LogInfoType.Normal, "电源ID: " + _idn);
                _psu.Reset();
            }
            if (!_psu.SetCurrentLimit(_parameterSetting.PathNumber, _parameterSetting.PsuCurrentLimit))
            {
                LogInfo(LogInfoType.Error, "设置电源输出电流上限失败!");
                return ExecOkError.Error;
            }

            if (!_psu.SetVoltage(_parameterSetting.PathNumber, _parameterSetting.PsuVoltage))
            {
                LogInfo(LogInfoType.Error, "设置电源输出电压失败!");
                return ExecOkError.Error;
            }

            Thread.Sleep((int)(_parameterSetting.DelayTimeBeforePowerOn * 1000));
            LogInfo(LogInfoType.Normal, $"上电前延时 {_parameterSetting.DelayTimeBeforePowerOn} 秒");

            if (!_psu.SetOutputStatus(_parameterSetting.PathNumber, true))
            {
                LogInfo(LogInfoType.Error, "打开电源失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogInfo(LogInfoType.Notify, "电源已打开");

            }

            LogInfo(LogInfoType.Normal, $"上电后延时 {_parameterSetting.DelayTimeAfterPowerOn} 秒再继续后续测试");
            for (int i = 0; i < _parameterSetting.DelayTimeAfterPowerOn; i++)
            {
                LogTip(LogTipType.Notify, string.Format("上电后等待 ({0} s)...", _parameterSetting.DelayTimeAfterPowerOn - i));
                Thread.Sleep(1000);
            }
            LogTip(LogTipType.Notify, string.Format("等待结束……"));


            if (!_psu.GetCurrent(_parameterSetting.PathNumber, out _current))
            {
                LogInfo(LogInfoType.Error, "读取电流失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogInfo(LogInfoType.Normal, $"读取电流值: {_current:F3} A");
            }
            LogResult(LogResultType.Normal,
                "上电后电流值", 
                _current, _current.ToString("f2"),"A",
                _limitSetting.PsuCurrent);
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
