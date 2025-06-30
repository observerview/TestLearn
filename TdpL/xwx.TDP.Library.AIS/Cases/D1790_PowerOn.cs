using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestManager.Extern.CoreCase;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.Common;
using xwx.TDP.Library.AIS.Devs;

namespace xwx.TDP.Library.AIS.Cases
{
    [Category("电源控制"),
     DisplayName("电源D1790上电"),
     Description("仅上电"),
    ]
    public class D1790_PowerOn : CoreCaseCommon
    {
        DH1790Psu _psu1790 = new DH1790Psu();
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

            private string _psuIpAddress = "192.168.1.10";
            [Category("A.程控电源设置"),
             DisplayName("A04.该电源IP地址"),
             Description("设置该电源IP地址"),
             DefaultValue(typeof(double), "192.168.1.10")
            ]
            public string PsuIpAddress
            {
                get { return _psuIpAddress; }
                set { _psuIpAddress = value; }
            }

            private int _psuPort = 1790;
            [Category("A.程控电源设置"),
             DisplayName("A05.该电源端口号"),
             Description("设置程控电源控制端口号"),
             DefaultValue(typeof(int), "1790")
            ]
            public int PsuPort
            {
                get { return _psuPort; }
                set { _psuPort = value; }
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
            if (!_psu1790.Initialize(_parameterSetting.PsuIpAddress,_parameterSetting.PsuPort))
            {
                LogInfo(LogInfoType.Error, string.Format("连接电源{0}失败!",_parameterSetting.PsuIpAddress));
                return ExecOkError.Error;
            }

            if (!_psu1790.GetIdn(out _idn))
            {
                LogInfo(LogInfoType.Error, "获取电源信息失败!");
                return ExecOkError.Error;
            }
            else
            {
                LogInfo(LogInfoType.Normal, "电源ID: " + _idn);
            }
            if (!_psu1790.SetCurrentLimit(_parameterSetting.PathNumber, _parameterSetting.PsuCurrentLimit))
            {
                LogInfo(LogInfoType.Error, "设置电源输出电流上限失败!");
                return ExecOkError.Error;
            }

            if (!_psu1790.SetVoltage(_parameterSetting.PathNumber, _parameterSetting.PsuVoltage))
            {
                LogInfo(LogInfoType.Error, "设置电源输出电压失败!");
                return ExecOkError.Error;
            }

            if (!_psu1790.SetOutputStatus(_parameterSetting.PathNumber, true))
            {
                LogInfo(LogInfoType.Error, "打开电源失败!");
                return ExecOkError.Error;
            }

            else
            {
                LogInfo(LogInfoType.Notify, "电源已打开");

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
