using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor
{
    public class SequenceSelectedToEditEventArgs
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
        public SequenceSelectedToEditEventArgs(string sequenceXmlFileName)
        {
            this._sequenceXmlFileName = sequenceXmlFileName;
        }
        private string _sequenceXmlFileName;
    }
}
