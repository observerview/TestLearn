using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Extern;
using TestManager.Utility.PropertyGridEx;
using TestManager.Extern.Interface;

namespace xwx.TDP.Library.BaseCase
{
    [Category("Demo Case")]
    [DisplayName("Demo_PASSorFail")]
    [Description("Demo Case, result is pass or fail")]
    public class DemoTestCaseFassFail : CoreCase
    {
        #region Parameter Define
        public class ParameterSetting : ConfigBase
        {
            #region imput Parameter
            private int _inputValue = -50;
            [Category("A.测试参数"),
             DisplayName("A01.信号电平(dBm)"),
             Description("这是一个关于测试结果是pass/fail的测试case示例，这里设置的值会直接作为结果去limit里面判定。"),
             DefaultValue(typeof(int), "-50")
            ]
            public int InputValue
            {
                get { return this._inputValue; }
                set{_inputValue = value;}
            }
            #endregion

        }
        #endregion Parameter Define

        #region Limit Define
        private class LimitSetting : ConfigBase
        {
            private ValueLimit _resultLimit = new ValueLimit("[-10,10] | double");
            [Category("A.测试指标"),
             DisplayName("A01.测试指标范围"),
             Description("测试指标范围，对于该测试demo，设置的input在limit范围内，则测试判定pass，否则fail"),
             DefaultValue(typeof(ValueLimit), "[-10,10] | double"),
            ]
            public ValueLimit ResultLimit
            {
                get { return _resultLimit; }
                set { _resultLimit = value; }
            }
        }
        #endregion Limit Define

        #region Limit Getter and Setter
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
        #endregion Limit Getter and Setter

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


        public override ExecOkError PreExec()
        {
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            //这个方法会在running界面显示测试结果
            LogResult(LogResultType.Normal,
                "测试结果",
                _parameterSetting.InputValue,
                _parameterSetting.InputValue.ToString("f2"),
                "单位",
                _limitSetting.ResultLimit);

            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            return ExecOkError.OK;
        }

    }
}
