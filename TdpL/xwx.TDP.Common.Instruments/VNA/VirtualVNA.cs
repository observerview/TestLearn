using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static xwx.TDP.Common.Instruments.MessageInstruments;

namespace xwx.TDP.Common.Instruments.VNA
{
    [DisplayName("Simulated NA"),
     FirmwareVersion("N/A"),
     Description("Simulated NA")]
    public class VirtualVNA : VNA
    {
        public override bool GetIdn(out string idn)
        {
            idn = "Simulated Network Analyzer";
            return true;
        }

        public override bool Preset()
        {
            return true;
        }

        public override bool SendCommand(string cmd)
        {
            return true;
        }

        public override void SendCommands(string[] cmds)
        {
        }
    }
}
