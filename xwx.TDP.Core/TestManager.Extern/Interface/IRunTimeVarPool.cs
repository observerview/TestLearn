using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Extern.Interface
{
    public interface IRunTimeVarPool : IRunTimeVarPoolReadOnly
    {
        new object this[string key] { get; set; }

        void Add(string key, object value);

        void Remove(string key);

    }
}
