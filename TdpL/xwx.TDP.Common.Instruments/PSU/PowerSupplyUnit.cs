using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Common.Instruments.PSU
{
    public abstract class PowerSupplyUnit: MessageInstruments
    {
        public virtual uint NumberOfPath
        {
            get { return 1; }
            set { }
        }
        public virtual double[] MaxVoltageV
        {
            get;
            set;
        }
        public virtual double[] MaxCurrentA
        {
            get;
            set;
        }
        protected bool ValidatePathNo(uint pathNo)
        {
            if (pathNo > NumberOfPath)
            {
                errString = string.Format("Only {0} voltage path supported.", NumberOfPath);
                return false;
            }
            return true;
        }

        public abstract bool GetIdn(out string idn);
        public abstract bool Reset();
        public abstract bool SetVoltage(uint pathNo, double voltageV);
        public abstract bool GetVoltage(uint pathNo, out double voltageV);
        public abstract bool SetCurrentLimit(uint pathNo, double currentA);
        public abstract bool GetCurrent(uint pathNo, out double currentA);
        public abstract bool SetOutputStatus(uint pathNo, bool status);
        protected bool[] powerOnFlags;

        public bool[] PowerOnFlags
        {
            get
            {
                if (powerOnFlags.Length <= 1)
                {
                    return new bool[] { true };
                }
                return powerOnFlags;
            }
        }

        public PowerSupplyUnit()
        {
            powerOnFlags = new bool[this.NumberOfPath];
        }
    }
}
