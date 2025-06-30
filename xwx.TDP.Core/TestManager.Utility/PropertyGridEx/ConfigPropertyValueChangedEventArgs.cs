using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Utility.PropertyGridEx
{
    public class ConfigPropertyValueChangedEventArgs : EventArgs
    {
        private object _oldValue;

        private object _newValue;

        public object OldValue { get { return _oldValue; } set { _oldValue = value; } } 

        public object NewValue { get { return _newValue; } set { _newValue = value; } }

        public ConfigPropertyValueChangedEventArgs(object oldValue, object newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }
    }
}
