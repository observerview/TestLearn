using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Editor.Engine
{
    internal class TdpTestCase
    {
        

        public TestCase SelfTestCase
        {
            get
            {
                return _selfTestCase;
            }
            set
            {
                _selfTestCase = value;
            }
        }
        public List<TdpTestCase> ContainedTdpTestCase
        {
            get
            {
                return _containedTdpTestCase;
            }
        }
        private TestCase _selfTestCase;
        private List<TdpTestCase> _containedTdpTestCase = new List<TdpTestCase>();
    }
}
