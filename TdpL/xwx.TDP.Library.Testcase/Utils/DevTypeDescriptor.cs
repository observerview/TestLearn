using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwx.TDP.Library.Common.Utils
{
    public class DevTypeDescriptor : CustomTypeDescriptor
    {
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return base.GetProperties(attributes);
        }
    }
}
