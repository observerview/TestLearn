using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static xwx.TDP.Common.Instruments.MessageInstruments;

namespace xwx.TDP.Common.Instruments.PSU
{
    [DisplayName("N5700"),
    FirmwareVersion("N/A"),
    Description("Keysight 5700系列，如N5747A")]
    public class N5700A :PowerSupplyUnit
    {
        private uint _numberOfPath = 1;
        private double[] _maxVoltageV = new double[] { 48 };
        public override uint NumberOfPath
        {
            get
            {
                return _numberOfPath;
            }
            set
            {
                _numberOfPath = value;
                _maxVoltageV = new double[value];
                for (int i = 0; i < _maxVoltageV.Length; i++)
                {
                    _maxVoltageV[i] = 28;
                }
            }
        }
        public override double[] MaxVoltageV
        {
            get
            {
                return _maxVoltageV;
            }
            set
            {
                if (value.Length != _numberOfPath)
                {
                    throw new Exception(string.Format("Only {0} voltage path supported", _numberOfPath));
                }
                else
                {
                    _maxVoltageV = value;
                }
            }
        }

        public override bool GetIdn(out string idn)
        {
            return QueryString("*IDN?", out idn);
        }

        public override bool Reset()
        {
            return WriteString("*RST");

        }
        public override bool SetVoltage(uint pathNo, double voltageV)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            //if (voltageV > this.MaxVoltageV[pathNo])
            //{
            //    this.errString = string.Format("path {0}set the voltage{1} out of maxlevel{2}!", pathNo, voltageV,
            //                                   this.MaxVoltageV[pathNo]);
            //    return false;
            //}
            return WriteString(string.Format("VOLT {0:f}", voltageV));
        }
        public override bool GetVoltage(uint pathNo, out double voltageV)
        {
            voltageV = 0;
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return QueryDouble("MEAS:VOLT?", out voltageV);
        }
        public override bool SetCurrentLimit(uint pathNo, double currentA)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return WriteString(string.Format("CURR {0:f}", currentA));
        }
        public override bool GetCurrent(uint pathNo, out double currentA)
        {
            currentA = 0;
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }
            return QueryDouble("MEAS:CURR?", out currentA);
        }
        /// <summary>
        /// Sets the output status.
        /// </summary>
        /// <param name="pathNo">The path No. = 1</param>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <returns></returns>
        public override bool SetOutputStatus(uint pathNo, bool status)
        {
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }

            if (status)
            {
                return WriteString("OUTP ON");

            }
            else
            {
                return WriteString("OUTP OFF");
            }

        }

    }
}
