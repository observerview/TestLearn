using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Threading;
using TestManager.Extern.Interface;
using TestManager.Utility.PropertyGridEx;
using xwx.TDP.Library.AIS.Devs;
using xwx.TDP.Library.Common;

namespace xwx.TDP.Library.AIS.Cases.Cases
{
    [Category("�������"),
     DisplayName("����AIS��̨ģ����_��ָ��"),
     Description("����AIS��̨ģ�������Ʒ�ָ��"),
    ]
    public class Send_3hAis_cmd : CoreCaseCommon
    {
        #region Private Fields
        private string _idn = "";
        public enum Modechoose
        {
            local,
            remote
        }
        #endregion

        #region Parameter Setting
        private class ParameterSetting : ConfigBase
        {
            [Category("A.ͨ������"),
             DisplayName("A01.������ʣ�dBm"),
             Description("����������ʣ��ù���ֱ�����õ���ӿڵĹ��ʣ������ڲ��Լ����Һ����RF·�����ʵ��AIS��̨�����Ĺ���"),
             DefaultValue(typeof(double), "0"),
            ]
            public double OutPutPower { get; set; } = -114;

            [Category("A.ͨ������"),
             DisplayName("A02.RF��·˥����dBm"),
             Description("��·�ϵ�˥��ֵ��ֱ������������������ʾ����"),
             DefaultValue(typeof(double), "0"),
            ]
            public double RfAttenuation { get; set; } = 95;

            [Category("A.ͨ������"),
             DisplayName("A03.���͵ı�����,10��һ��"),
             Description("���͵ı�����������ֵx10"),
             DefaultValue(typeof(int), "100"),
            ]
            public int MsgCount { get; set; } = 100;

            [Category("A.ͨ������"),
             DisplayName("A03.���͵ı���ʱ����(ms)"),
             Description("���͵ı���ʱ��������λ����"),
             DefaultValue(typeof(int), "500"),
            ]
            public int MsgTimeInter { get; set; } = 500;


            [Category("A.ͨ������"),
             DisplayName("A04.�ŵ�ѡ��"),
             Description("�ŵ�ѡ��,ѡ��local�ǳ���AIS��ѡ��remote��Զ��AIS"),
             DefaultValue(typeof(Modechoose), "local"),
            ]
            public Modechoose MsgMode { get; set; } = Modechoose.local;


            [Category("B.AIS1����"),
            DisplayName("B01.AIS1Ƶƫ"),
            Description("Ƶƫ����λ��Hz��ȡֵ��Χ�� - 5500~5500"),
            DefaultValue(typeof(double), "0.0"),
           ]
            public double AIS1FrequencyOffset { get; set; } = 0.0;

            [Category("B.AIS1����"),
             DisplayName("B02.AIS1��Ϣ�ַ���"),
             Description("�����豸�Դ�������ɣ�Ĭ����,MMSI1=11111111,����״̬=0����ת�ٶ�=5���Եغ���=6.0���Եغ���=7.0��λ��׼ȷ��=0������=121.000��γ��=37.500��ʵ�ʺ���=8��RAMI��־=0���صز���ָʾ��=0��ʱ���=63"),
             DefaultValue(typeof(string), "2058BEF53840C343942707A8AE540062207E000000FC4B"),
            ]
            public string MMSI1_string { get; set; } = "2058BEF53840C343942707A8AE540062207E000000FC4B";


            [Category("C.AIS2����"),
           DisplayName("C01.AIS2Ƶƫ"),
           Description("Ƶƫ����λ��Hz��ȡֵ��Χ�� - 5500~5500"),
           DefaultValue(typeof(double), "0.0"),
          ]
            public double AIS2FrequencyOffset { get; set; } = 0.0;

            [Category("C.AIS2����"),
             DisplayName("C02.AIS2��Ϣ�ַ���"),
             Description("�����豸�Դ�������ɣ�Ĭ����,MMSI1=11111111,����״̬=0����ת�ٶ�=5���Եغ���=6.0���Եغ���=7.0��λ��׼ȷ��=0������=121.000��γ��=37.500��ʵ�ʺ���=8��RAMI��־=0���صز���ָʾ��=0��ʱ���=63"),
             DefaultValue(typeof(string), "2058BEF53840C343942707A8AE540062207E000000FC4B"),
            ]
            public string MMSI2_string { get; set; } = "2058BEF53840C343942707A8AE540062207E000000FC4B";



        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // ����ֵʾ��
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
            return ExecOkError.OK;
        }

        public override ExecOkError Exec()
        {
            uint msg_type_value = 1;
            if(_parameterSetting.MsgMode == Modechoose.remote)
            {
                msg_type_value = 27;
            }
            // check if settings are ok
            // check power and attenuation value, the power from DUT must -30 to 0
            double realpower = _parameterSetting.OutPutPower + _parameterSetting.RfAttenuation;
            if (realpower > 0 || realpower < -30)
            {
                LogInfo(LogInfoType.Warning, string.Format("����Ŀǰ�����ã��豸�����������{0}dBm,�豸����ķ�Χ��-30~0dbm�����ܳ��������Χ"), realpower.ToString());
                return ExecOkError.Error;
            }

            // set send mode AIS/LAIS
            bool mode = true;
            string modeName = string.Empty;
            if (_parameterSetting.MsgMode == Modechoose.local)
            {
                mode = true;
                modeName = "����AIS";
            }
            else
            {
                mode = false;
                modeName = "Զ��AIS";
            }
            LogInfo(LogInfoType.Warning, string.Format("��ǰ���ã�������{0}�ź�", modeName));


            //set parameter
            device_setting_para setting_Para = new device_setting_para
            {
                is_repeat = false,
                is_normal = mode,
                tx_rate = 0,
                all_count= 0,
                para_1= new channel_setting_para
                {
                    channel_id = 0x01,
                    frequency_offset = 0,
                    msg_type = msg_type_value,
                    power = realpowerCal(realpower),
                    slot_phase = 200,
                    tx_counter = 20,
                    tx_internal = 1
                },
                para_2 = new channel_setting_para
                {
                    channel_id = 0x02,
                    frequency_offset = 0,
                    msg_type = msg_type_value,
                    power = realpowerCal(realpower),
                    slot_phase = 0,
                    tx_counter = 20,
                    tx_internal = 1
                }
                
            };

            //init 3h emulator.
            if (_3hAisEmulator.init_device())
            {
                LogInfo(LogInfoType.Normal, "��ʼ��3h��̨ģ�����ɹ����豸����: " + _idn);
                Thread.Sleep(1000);
            }
            else
            {
                LogInfo(LogInfoType.Notify, "��ʼ��3h��̨ģ����ʧ�ܣ��豸�����ã����Խ���: " + _idn);
                return ExecOkError.Error;
            }

            try
            {
                var setChannelRet = _3hAisEmulator.set_channel_param(setting_Para);

                var setTxRet =  _3hAisEmulator.set_tx_data(setting_Para, _parameterSetting.MMSI1_string, _parameterSetting.MMSI2_string);
            }
            catch(Exception ex)
            {
                LogTip(LogTipType.Warning, $"3����̨����ʧ��,ֹͣ���ԡ�ԭ�� " + ex.ToString());
                return ExecOkError.Error;
            }

            try
            {
                _3hAisEmulator.set_ferq_rate_param(0);
                for (int i = 0; i < _parameterSetting.MsgCount; i++)
                {
                    if (_3hAisEmulator.start_tx())
                    {
                        Thread.Sleep(_parameterSetting.MsgTimeInter);
                        LogTip(LogTipType.Normal, $"�ɹ����͵�{(i + 1) * 10}����Ϣ");
                    }
                    else
                    {
                        LogTip(LogTipType.Normal, $"������Ϣʧ��");
                        return ExecOkError.Error;
                    }
                }

            }
            catch(Exception ex)
            {
                LogTip(LogTipType.Normal, $"���ƴ�̨ģ��������" + ex.ToString());
                return ExecOkError.Error;
            }
            finally
            {
                _3hAisEmulator.stop_tx();
                //close 
                _3hAisEmulator.close_device();
            }
           
            
            return ExecOkError.OK;
        }

        public override ExecOkError PostExec()
        {
            // �ڴ˴���Ӳ��Ժ��������
            return ExecOkError.OK;
        }
        #endregion

        private short realpowerCal(double value)
        {
            double mulValue = value * 10;
            if (mulValue > short.MaxValue || mulValue < short.MinValue)
            {
                throw new OverflowException("����short�ķ�Χ��");
            }
            return (short)mulValue;
        }
    }
}