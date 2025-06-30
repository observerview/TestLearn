using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Library.AIS.Devs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct channel_setting_para
    {
        /// <summary>
        /// 通道的频道ID，范围1~2，1~2对应2个频道编号，以常规信道为例，1代表：161.975MHz,2代表：162.025MHz,0代表通道不使能
        /// </summary>
        public byte channel_id;
        /// <summary>
        /// 频偏（单位：Hz）取值范围： - 5500~5500
        /// </summary>
        public short frequency_offset;
        /// <summary>
        /// 功率（单位：0.1dBm）取值范围： - 300~0(对应 - 30~0dBm)
        /// </summary>
        public short power;
        /// <summary>
        /// 发射时间调整（单位：1us）取值范围：0~5000
        /// </summary>
        public ushort slot_phase;
        /// <summary>
        /// 发送次数 取值范围：1~37（!!!注意发射报文+发送间隔总时间不要超过1s(约37时隙)）
        /// </summary>
        public ushort tx_counter;
        /// <summary>
        /// 发送间隔（单位：时隙）0~35（!!!注意发射报文+发送间隔总时间不要超过1s(约37时隙)）
        /// </summary>
        public ushort tx_internal;
        /// <summary>
        /// 报文类型，取值：1~27号报文，其中：16/17/20/22/23号报文不发送
        /// </summary>
        public uint msg_type;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct device_setting_para
    {
        /// <summary>
        /// 报文是否自动重复发送
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool is_repeat;
        /// <summary>
        /// 常规信道true、远程信道false
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool is_normal;
        /// <summary>
        /// 多普勒变化率，单位：HZ/次，为0时无效
        /// </summary>
        public int tx_rate;
        /// <summary>
        /// 总计发射时间，为0时无效。此字段预留，无效！！！
        /// </summary>
        public int all_count;
        /// <summary>
        /// 通道1发送参数
        /// </summary>
        public channel_setting_para para_1;
        /// <summary>
        /// 通道2发送参数
        /// </summary>
        public channel_setting_para para_2;
    }
    public static class _3hAisEmulator
    {
        [DllImport("lib2A_simulator.dll",CallingConvention =CallingConvention.Cdecl)]
        public extern static bool init_device();

        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        //public extern static bool set_channel_param(IntPtr para);
        public extern static bool set_channel_param(device_setting_para para);

        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool set_ferq_rate_param(int tx_rate);

        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool set_tx_data(device_setting_para para,string channel1,string channel2);

        //[DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        //[return: MarshalAs(UnmanagedType.I1)]
        //public static extern bool set_tx_data(ref device_setting_para pare , [MarshalAs(UnmanagedType.LPStr)] string channel1, [MarshalAs(UnmanagedType.LPStr)] string channel2);


        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool start_tx();
        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool stop_tx();

        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int get_device_temp();

        [DllImport("lib2A_simulator.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool close_device();


    }
}
