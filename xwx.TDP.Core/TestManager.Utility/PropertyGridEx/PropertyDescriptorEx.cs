using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Utility.PropertyGridEx
{
    internal class PropertyDescriptorEx : PropertyDescriptor
    {
        private object defaultValue;

        private PropertyDescriptor _target;

        public override Type ComponentType
        {
            get
            {
                return _target.ComponentType;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return _target.IsReadOnly;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return _target.PropertyType;
            }
        }

        public PropertyDescriptorEx(PropertyDescriptor target, Attribute[] attributes)
            : base(target, attributes)
        {
            _target = target;
            for (int i = 0; i < _target.Attributes.Count; i++)
            {
                if (_target.Attributes[i] is DefaultValueAttribute)
                {
                    defaultValue = (_target.Attributes[i] as DefaultValueAttribute).Value;
                }
            }
        }

        public override bool CanResetValue(object component)
        {
            if (defaultValue != null)
            {
                return !GetValue(component).Equals(defaultValue);
            }
            return false;
        }

        public override void ResetValue(object component)
        {
            if (defaultValue != null)
            {
                SetValue(component, defaultValue);
            }
        }

        public override object GetValue(object component)
        {
            return _target.GetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            if (value.GetType().IsArray)
            {
                _target.SetValue(component, value);
            }
            else if (value.GetType().GetInterface("ICollection") != null)
            {
                IList list = Activator.CreateInstance(_target.PropertyType) as IList;
                IEnumerator enumerator = (value as ICollection).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                }
                _target.SetValue(component, list);
            }
            else
            {
                _target.SetValue(component, value);
            }
            OnValueChanged(component, EventArgs.Empty);
        }

        public override bool ShouldSerializeValue(object component)
        {
            if (defaultValue != null)
            {
                return !GetValue(component).Equals(defaultValue);
            }
            return true;
        }
    }
}
