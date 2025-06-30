using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Common.Instruments.VNA
{
    public abstract class VNA : MessageInstruments
    {
        //public enum SParameterEnum
        //{
        //    S11,
        //    S12,
        //    S21,
        //    S22,
        //}
        public abstract bool GetIdn(out string idn);

        //public abstract bool Wait();

        public abstract bool Preset();

        public abstract bool SendCommand(string cmd);
        public abstract void SendCommands(string[] cmds);

        //public abstract bool SetFreqCenter(int channelNo, double freqHz);
        //public abstract bool GetFreqCenter(int channelNo, out double freqHz);
        //public abstract bool SetFreqStart(int channelNo, double freqHz);
        //public abstract bool GetFreqStart(int channelNo, out double freqHz);
        //public abstract bool SetFreqStop(int channelNo, double freqHz);
        //public abstract bool GetFreqStop(int channelNo, out double freqHz);
        //public abstract bool SetFreqSpan(int channelNo, double freqHz);
        //public abstract bool GetFreqSpan(int channelNo, out double freqHz);
        //public abstract bool SetIfBandwidth(int channelNo, double freqHz);
        //public abstract bool GetIfBandwidth(int channelNo, out double freqHz);
    }
}
