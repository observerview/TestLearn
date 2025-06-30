using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor
{
    public class SequenceInfoChangingEventArgs
    {
        public string SequenceXmlFileName
        {
            get
            {
                return this._sequenceXmlFileName;
            }
            set
            {
                this._sequenceXmlFileName = value;
            }
        }
        public SequenceInfoChangingEventArgs(string sequenceXmlFileName)
        {
            this._sequenceXmlFileName = sequenceXmlFileName;
        }
        private string _sequenceXmlFileName;
    }
}
