using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Common.Instruments.PSU
{
    [DisplayName("Simulated PSU"),
    FirmwareVersion("N/A"),
    Description("Instrument driver for simulated power supply unit.")]
    public class SimulatePSU : PowerSupplyUnit
    {
        private int _timeOutSession = 1000;
        private int _defaultBufferSize = 16 * 1024;
        private string _resourceName = string.Empty;

        public override int TimeOutSession
        {
            get
            {
                return _timeOutSession;
            }
            set
            {
                _timeOutSession = value;
            }
        }
        public override int DefaultBufferSize
        {
            get
            {
                return _defaultBufferSize;
            }
            set
            {
                _defaultBufferSize = value;
            }
        }

        public override string ResourceName
        {
            get { return _resourceName; }
        }

        public override bool Initialize(string resource)
        {
            _resourceName = resource;
            return true;
        }
        public override bool Initialize(string resource, string remoteServer)
        {
            return true;
        }

        public override bool GetIdn(out string idn)
        {
            idn = "Simulate Power Supply Unit";
            return true;
        }
        public override bool Reset()
        {
            return true;
        }
        public override bool GetCurrent(uint pathNo, out double currentA)
        {
            currentA = 0;
            if (!this.ValidatePathNo(pathNo))
            {
                return false;
            }
            currentA = (new Random().NextDouble() * 0.5) + 0.5;

            return true;
        }

        public override bool GetVoltage(uint pathNo, out double voltageV)
        {
            voltageV = 0;
            if (!ValidatePathNo(pathNo))
            {
                return false;
            }

            voltageV = new Random().NextDouble() * this.MaxVoltageV[pathNo];//随机返回一个范围内电压值.  dummy数据可能固定不正常值更好？？
            return true;
        }

        public override bool SetCurrentLimit(uint pathNo, double currentA)
        {
            if (!this.ValidatePathNo(pathNo))
            {
                return false;
            }
            return true;
        }

        public override bool SetOutputStatus(uint pathNo, bool status)
        {
            if (!this.ValidatePathNo(pathNo))
            {
                return false;
            }
            return true;
        }

        public override bool SetVoltage(uint pathNo, double voltageV)
        {
            if (!this.ValidatePathNo(pathNo))
            {
                return false;
            }

            return true;
        }
    }
}
