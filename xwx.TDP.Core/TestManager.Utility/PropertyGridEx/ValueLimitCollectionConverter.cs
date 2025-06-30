using System;
using System.ComponentModel;
using System.Globalization;

namespace TestManager.Utility.PropertyGridEx
{
	public class ValueLimitCollectionConverter : TypeConverter
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
			if (sourceType == typeof(string))
			{
				return true;
			}
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
				ValueLimitCollection valueLimitCollection = new ValueLimitCollection(value as string, (_oldValue == null || !(_oldValue is ValueLimitCollection)) ? null : (_oldValue as ValueLimitCollection).GlobeValueType);
				if (_oldValue != null && _oldValue is ValueLimitCollection && valueLimitCollection.GlobeValueType == null)
				{
					if (!valueLimitCollection.IndividualVauleTypeSpecified)
					{
						valueLimitCollection.GlobeValueType = (_oldValue as ValueLimitCollection).GlobeValueType;
					}
					else
					{
						valueLimitCollection.ValueType = (_oldValue as ValueLimitCollection).GlobeValueType;
					}
				}
				if (valueLimitCollection.IsValid)
				{
					return valueLimitCollection;
				}
				return _oldValue as ValueLimitCollection;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
