using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Extern.Interface
{
    public interface IRunTimeVarPoolReadOnly
    {
        object this[string key] { get; }

        bool ContainKey(string key);

        bool ContainValue(object value);
    }
}
