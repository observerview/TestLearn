using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Extern.Interface;

namespace xwx.TDP.Editor.Engine
{
    internal class RunTimeVarPool: IRunTimeVarPoolReadOnly, IRunTimeVarPool
    {
        private Hashtable _varPool = new Hashtable();

        public void Add(string key, object value)
        {
            this._varPool.Add(key, value);
        }
        public void Clear()
        {
            this._varPool.Clear();
        }
        public bool ContainKey(string key)
        {
            return this._varPool.ContainsKey(key);
        }
        public bool ContainValue(object value)
        {
            return this._varPool.ContainsValue(value);
        }
        public void Remove(string key)
        {
            this._varPool.Remove(key);
        }
        public object this[string key]
        {
            get
            {
                return this._varPool[key];
            }
            set
            {
                this._varPool[key] = value;
            }
        }
    }
}
