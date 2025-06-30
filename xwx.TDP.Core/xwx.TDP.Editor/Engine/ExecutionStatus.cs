using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern;
using TestManager.Extern.Interface;
using xwx.TDP.Editor.Properties;

namespace xwx.TDP.Editor.Engine
{
    internal class ExecutionStatus
    {
        private uint _passedTimes;

        private uint _failedTimes;

        private uint _errorTimes;

        private uint _skippedTimes;

        private PassFailSkipError _lastTimeFinalPassFailSkipError = PassFailSkipError.Passed;

        private List<PassFailSkipError> _passFailSkipErrorHistory = new List<PassFailSkipError>();

        public List<PassFailSkipError> PassFailSkipErrorHistory
        {
            get
            {
                return this._passFailSkipErrorHistory;
            }
        }
        public PassFailSkipError LastTimeFinalPassFailSkipError
        {
            get
            {
                return this._lastTimeFinalPassFailSkipError;
            }
            set
            {
                this._lastTimeFinalPassFailSkipError = value;
            }
        }
        public uint SkippedTimes
        {
            get
            {
                return this._skippedTimes;
            }
            set
            {
                this._skippedTimes = value;
            }
        }
        public uint ErrorTimes
        {
            get
            {
                return this._errorTimes;
            }
            set
            {
                this._errorTimes = value;
            }
        }
        public uint PassedTimes
        {
            get
            {
                return this._passedTimes;
            }
            set
            {
                this._passedTimes = value;
            }
        }
        public uint FailedTimes
        {
            get
            {
                return this._failedTimes;
            }
            set
            {
                this._failedTimes = value;
            }
        }
        public uint TotalExecutedTimes
        {
            get
            {
                return this._passedTimes + this._failedTimes + this._errorTimes + this._skippedTimes;
            }
        }
        /// <summary>
        /// todo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(new string[]
            {
                string.Format("{0} " + "Resources.LNG_TDP_Engine_ExecutionStatus_Executed", this.TotalExecutedTimes),
                (this.ErrorTimes == 0U) ? string.Empty : string.Format(", {0} " + "Resources.LNG_TDP_Engine_ExecutionStatus_Error", this.ErrorTimes),
                (this.SkippedTimes == 0U) ? string.Empty : string.Format(", {0} " + "Resources.LNG_TDP_Engine_ExecutionStatus_Skipped", this.SkippedTimes),
                (this.PassedTimes == 0U) ? string.Empty : string.Format(", {0} " + "Resources.LNG_TDP_Engine_ExecutionStatus_Passed", this.PassedTimes),
                (this.FailedTimes == 0U) ? string.Empty : string.Format(", {0} " + "Resources.LNG_TDP_Engine_ExecutionStatus_Failed", this.FailedTimes)
            });
        }
        public void ResetPassFailRatio()
        {
            this._passedTimes = 0U;
            this._failedTimes = 0U;
            this._errorTimes = 0U;
            this._skippedTimes = 0U;
            this._passFailSkipErrorHistory.Clear();
        }
        public void ResetTempVariable()
        {
            this._lastTimeFinalPassFailSkipError = PassFailSkipError.Passed;
        }
    }
}
