using System.Collections.Specialized;
using System.ComponentModel;

namespace TestManager.Utility.PropertyGridEx
{
    public class ConfigBase
    {
        public event ConfigPropertyValueChangedEventHandler ConfigPropertyValueChanged;
        public bool SetPropertiesValue(NameValueCollection names_values)
        {
            bool result = true;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
            PropertyDescriptor propertyDescriptor = null;
            object obj = null;
            for (int i = 0; i < names_values.Count; i++)
            {
                if ((propertyDescriptor = properties[names_values.GetKey(i)]) == null)
                {
                    result = false;
                    continue;
                }
                PropertyDescriptorEx propertyDescriptorEx = new PropertyDescriptorEx(propertyDescriptor, null);
                if (propertyDescriptorEx == null)
                {
                    result = false;
                    continue;
                }
                if ((obj = TypeConverterEx.ChangeStrongType(names_values[i], propertyDescriptorEx.PropertyType)) == null)
                {
                    result = false;
                    continue;
                }
                try
                {
                    propertyDescriptorEx.SetValue(this, obj);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        public bool GetPropertiesValue(out NameValueCollection names_values)
        {
            NameValueCollection display_names_values = new NameValueCollection();
            return GetPropertiesValue(out names_values, out display_names_values);
        }

        public bool GetPropertiesValue(out NameValueCollection names_values, out NameValueCollection display_names_values)
        {
            names_values = new NameValueCollection();
            display_names_values = new NameValueCollection();
            bool result = true;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
            properties.Sort();
            PropertyDescriptor propertyDescriptor = null;
            string text = null;
            for (int i = 0; i < properties.Count; i++)
            {
                if ((propertyDescriptor = properties[i]) == null)
                {
                    result = false;
                    continue;
                }
                PropertyDescriptorEx propertyDescriptor2 = new PropertyDescriptorEx(propertyDescriptor, null);
                propertyDescriptor2 = TypeDescriptorEx.GetExtendedPropertyDescriptor(propertyDescriptor2);
                if (propertyDescriptor2 == null)
                {
                    result = false;
                    continue;
                }
                object obj = null;
                try
                {
                    obj = propertyDescriptor2.GetValue(this);
                }
                catch
                {
                    result = false;
                    continue;
                }
                text = ((obj == null) ? string.Empty : (TypeConverterEx.ChangeStrongType(obj, typeof(string)) as string));
                string text2 = ((obj == null) ? string.Empty : (propertyDescriptor2.Converter.ConvertTo(null, null, obj, typeof(string)) as string));
                if (text == null || text2 == null)
                {
                    result = false;
                    continue;
                }
                names_values.Add(propertyDescriptor2.Name, text);
                display_names_values.Add(propertyDescriptor2.DisplayName, text2);
            }
            return result;
        }

        protected void OnConfigPropertyValueChanged(object oldValue, object newValue)
        {
            if (this.ConfigPropertyValueChanged != null)
            {
                this.ConfigPropertyValueChanged(this, new ConfigPropertyValueChangedEventArgs(oldValue, newValue));
            }
        }
    }
}
