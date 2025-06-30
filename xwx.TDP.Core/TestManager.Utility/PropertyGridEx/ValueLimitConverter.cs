using System;
using System.ComponentModel;
using System.Globalization;

namespace TestManager.Utility.PropertyGridEx
{
	public class ValueLimitConverter : TypeConverterEx
	{
		private object _oldValue;

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			_oldValue = value;
			if (destinationType == typeof(string))
			{
				return value.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				ValueLimit valueLimit = null;
				valueLimit = new ValueLimit(value as string);
				if (valueLimit.ValueType == null && _oldValue != null)
				{
					valueLimit.ValueType = (_oldValue as ValueLimit).ValueType;
				}
				if (valueLimit.IsValid)
				{
					return valueLimit;
				}
				return _oldValue as ValueLimit;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
