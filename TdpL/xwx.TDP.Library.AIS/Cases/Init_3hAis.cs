using System;
using System.ComponentModel;
using System.Threading;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.AIS.Devs;
using xwx.TDP.Library.Common;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("�������"),
     DisplayName("����AIS��̨ģ����_��ʼ��"),
     Description("����AIS��̨ģ����_��ʼ������ʼ��ʧ��ֱ�ӱ���"),
    ]
    public class Init_3hAis : CoreCaseCommon
    {
        #region Private Fields
        private string _idn = "";
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            // ����ʾ��
            //private double _parameterExample = 0.0;
            //[Category("A.��������"),
            // DisplayName("A01.3H AIS�豸ID"),
            // Description("��������"),
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
            //// ����ֵʾ��
            //private ValueLimit _limitExample = new ValueLimit("[0,100] | double");
            //[Category("A.����ֵ��"),
            // DisplayName("A01.����ֵ����"),
            // Description("����ֵ����"),
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

            // �ڴ˴���Ӳ���ǰ��׼������
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            // �ڴ˴������Ҫ�����߼�

            if (_3hAisEmulator.init_device())
            {
                LogInfo(LogInfoType.Normal, "��ʼ��3h��̨ģ�����ɹ����豸����: " + _idn);
                Thread.Sleep(1000);
                var devTemp = _3hAisEmulator.get_device_temp();
                LogInfo(LogInfoType.Notify, $"3h��̨�豸�¶ȣ� {devTemp}�� " );
                _3hAisEmulator.close_device();
                LogInfo(LogInfoType.Normal, "�Ͽ��豸���ӣ������������ԡ� " + _idn);
                return ExecOkError.OK;
            }
            else
            {
                LogInfo(LogInfoType.Notify, "��ʼ��3h��̨ģ����ʧ�ܣ��豸�����ã����Խ���: " + _idn);
                return ExecOkError.Error;
            }
            
        }

        public override ExecOkError PostExec()
        {
            // �ڴ˴���Ӳ��Ժ��������
            return ExecOkError.OK;
        }
        #endregion
    }
}