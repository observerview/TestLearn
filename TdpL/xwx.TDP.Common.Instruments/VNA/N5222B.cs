using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Common.Instruments.VNA
{
    [DisplayName("N5222B"),
     FirmwareVersion("N/A"),
    Description("Instrument driver for Keysight N5222B")]
    public class N5222B : VNA
    {
        public override bool GetIdn(out string idn)
        {
            return QueryString("*IDN?", out idn);
        }

        public override bool Preset()
        {
            return WriteString(string.Format(":SYST:PRES"));
        }

        public override bool SendCommand(string cmd)
        {
            //send scpi
            var result = WriteString(string.Format(cmd));

            return result;
        }

        public override void SendCommands(string[] cmds)
        {
            throw new NotImplementedException();
        }
    }
}
