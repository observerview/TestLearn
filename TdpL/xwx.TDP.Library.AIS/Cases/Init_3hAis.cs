using System;
using System.ComponentModel;
using System.Threading;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.AIS.Devs;
using xwx.TDP.Library.Common;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("外设控制"),
     DisplayName("三航AIS船台模拟器_初始化"),
     Description("三航AIS船台模拟器_初始化，初始化失败直接报错"),
    ]
    public class Init_3hAis : CoreCaseCommon
    {
        #region Private Fields
        private string _idn = "";
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            // 参数示例
            //private double _parameterExample = 0.0;
            //[Category("A.参数设置"),
            // DisplayName("A01.3H AIS设备ID"),
            // Description("参数描述"),
            // DefaultValue(typeof(double), "0.0"),
            //]
            //public double ParameterExample
            //{
            //    get { return _parameterExample; }
            //    set { _parameterExample = value; }
            //}
        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            //// 限制值示例
            //private ValueLimit _limitExample = new ValueLimit("[0,100] | double");
            //[Category("A.限制值组"),
            // DisplayName("A01.限制值名称"),
            // Description("限制值描述"),
            // DefaultValue(typeof(ValueLimit), "[0,100] | double"),
            //]
            //public ValueLimit LimitExample
            //{
            //    get { return _limitExample; }
            //    set { _limitExample = value; }
            //}
        }
        #endregion

        #region Parameter & Limit Getter and Setter
        private ParameterSetting _parameterSetting = new ParameterSetting();
        public override ConfigBase CaseParameterSetting
        {
            get { return _parameterSetting; }
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
            get { return _limitSetting; }
            set
            {
                if (value != null && value is LimitSetting)
                {
                    _limitSetting = value as LimitSetting;
                }
            }
        }
        #endregion

        #region Test Case Implementation
        public override ExecOkError PreExec()
        {

            // 在此处添加测试前的准备工作
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            // 在此处添加主要测试逻辑

            if (_3hAisEmulator.init_device())
            {
                LogInfo(LogInfoType.Normal, "初始化3h船台模拟器成功，设备可用: " + _idn);
                Thread.Sleep(1000);
                var devTemp = _3hAisEmulator.get_device_temp();
                LogInfo(LogInfoType.Notify, $"3h船台设备温度： {devTemp}℃ " );
                _3hAisEmulator.close_device();
                LogInfo(LogInfoType.Normal, "断开设备连接，继续后续测试。 " + _idn);
                return ExecOkError.OK;
            }
            else
            {
                LogInfo(LogInfoType.Notify, "初始化3h船台模拟器失败，设备不可用，测试结束: " + _idn);
                return ExecOkError.Error;
            }
            
        }

        public override ExecOkError PostExec()
        {
            // 在此处添加测试后的清理工作
            return ExecOkError.OK;
        }
        #endregion
    }
}