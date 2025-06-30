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
    [Category("外设控制"),
     DisplayName("三航AIS船台模拟器_发指令"),
     Description("三航AIS船台模拟器控制发指令"),
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
            [Category("A.通用配置"),
             DisplayName("A01.输出功率，dBm"),
             Description("配置输出功率，该功率直接配置到达接口的功率，程序内部自己按找后面的RF路损计算实际AIS船台出来的功率"),
             DefaultValue(typeof(double), "0"),
            ]
            public double OutPutPower { get; set; } = -114;

            [Category("A.通用配置"),
             DisplayName("A02.RF线路衰减，dBm"),
             Description("链路上的衰减值，直接设置正数，负数表示增益"),
             DefaultValue(typeof(double), "0"),
            ]
            public double RfAttenuation { get; set; } = 95;

            [Category("A.通用配置"),
             DisplayName("A03.发送的报文数,10个一组"),
             Description("发送的报文数，配置值x10"),
             DefaultValue(typeof(int), "100"),
            ]
            public int MsgCount { get; set; } = 100;

            [Category("A.通用配置"),
             DisplayName("A03.发送的报文时间间隔(ms)"),
             Description("发送的报文时间间隔，单位毫秒"),
             DefaultValue(typeof(int), "500"),
            ]
            public int MsgTimeInter { get; set; } = 500;


            [Category("A.通用配置"),
             DisplayName("A04.信道选择"),
             Description("信道选择,选择local是常规AIS，选择remote是远程AIS"),
             DefaultValue(typeof(Modechoose), "local"),
            ]
            public Modechoose MsgMode { get; set; } = Modechoose.local;


            [Category("B.AIS1设置"),
            DisplayName("B01.AIS1频偏"),
            Description("频偏（单位：Hz）取值范围： - 5500~5500"),
            DefaultValue(typeof(double), "0.0"),
           ]
            public double AIS1FrequencyOffset { get; set; } = 0.0;

            [Category("B.AIS1设置"),
             DisplayName("B02.AIS1消息字符串"),
             Description("可用设备自带软件生成，默认是,MMSI1=11111111,导航状态=0，旋转速度=5，对地航速=6.0，对地航向=7.0，位置准确度=0，经度=121.000，纬度=37.500，实际航向=8，RAMI标志=0，特地操纵指示符=0，时间戳=63"),
             DefaultValue(typeof(string), "2058BEF53840C343942707A8AE540062207E000000FC4B"),
            ]
            public string MMSI1_string { get; set; } = "2058BEF53840C343942707A8AE540062207E000000FC4B";


            [Category("C.AIS2设置"),
           DisplayName("C01.AIS2频偏"),
           Description("频偏（单位：Hz）取值范围： - 5500~5500"),
           DefaultValue(typeof(double), "0.0"),
          ]
            public double AIS2FrequencyOffset { get; set; } = 0.0;

            [Category("C.AIS2设置"),
             DisplayName("C02.AIS2消息字符串"),
             Description("可用设备自带软件生成，默认是,MMSI1=11111111,导航状态=0，旋转速度=5，对地航速=6.0，对地航向=7.0，位置准确度=0，经度=121.000，纬度=37.500，实际航向=8，RAMI标志=0，特地操纵指示符=0，时间戳=63"),
             DefaultValue(typeof(string), "2058BEF53840C343942707A8AE540062207E000000FC4B"),
            ]
            public string MMSI2_string { get; set; } = "2058BEF53840C343942707A8AE540062207E000000FC4B";



        }
        #endregion

        #region Limit Setting
        private class LimitSetting : ConfigBase
        {
            // 限制值示例
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
                LogInfo(LogInfoType.Warning, string.Format("按照目前的设置，设备的输出功率是{0}dBm,设备输出的范围是-30~0dbm，不能超出这个范围"), realpower.ToString());
                return ExecOkError.Error;
            }

            // set send mode AIS/LAIS
            bool mode = true;
            string modeName = string.Empty;
            if (_parameterSetting.MsgMode == Modechoose.local)
            {
                mode = true;
                modeName = "常规AIS";
            }
            else
            {
                mode = false;
                modeName = "远程AIS";
            }
            LogInfo(LogInfoType.Warning, string.Format("当前配置，将发送{0}信号", modeName));


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
                LogInfo(LogInfoType.Normal, "初始化3h船台模拟器成功，设备可用: " + _idn);
                Thread.Sleep(1000);
            }
            else
            {
                LogInfo(LogInfoType.Notify, "初始化3h船台模拟器失败，设备不可用，测试结束: " + _idn);
                return ExecOkError.Error;
            }

            try
            {
                var setChannelRet = _3hAisEmulator.set_channel_param(setting_Para);

                var setTxRet =  _3hAisEmulator.set_tx_data(setting_Para, _parameterSetting.MMSI1_string, _parameterSetting.MMSI2_string);
            }
            catch(Exception ex)
            {
                LogTip(LogTipType.Warning, $"3航船台配置失败,停止测试。原因： " + ex.ToString());
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
                        LogTip(LogTipType.Normal, $"成功发送第{(i + 1) * 10}条消息");
                    }
                    else
                    {
                        LogTip(LogTipType.Normal, $"发送消息失败");
                        return ExecOkError.Error;
                    }
                }

            }
            catch(Exception ex)
            {
                LogTip(LogTipType.Normal, $"控制船台模拟器报错：" + ex.ToString());
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
            // 在此处添加测试后的清理工作
            return ExecOkError.OK;
        }
        #endregion

        private short realpowerCal(double value)
        {
            double mulValue = value * 10;
            if (mulValue > short.MaxValue || mulValue < short.MinValue)
            {
                throw new OverflowException("超出short的范围。");
            }
            return (short)mulValue;
        }
    }
}