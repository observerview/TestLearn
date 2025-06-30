using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace TestManager.Utility.PropertyGridEx
{
	public class EnumConverterEx : EnumConverter
	{
		protected Type _enumValueType;

		public EnumConverterEx(Type type)
			: base(type.GetType())
		{
			_enumValueType = type;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Enum && destinationType == typeof(string))
			{
				return EnumDisplayNameAttribute.GetEnumerationDisplayName((Enum)value);
			}
			if (value is string && destinationType == typeof(string))
			{
				return EnumDisplayNameAttribute.GetEnumerationDisplayName(_enumValueType, (string)value);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				object enumerationValue = EnumDisplayNameAttribute.GetEnumerationValue(_enumValueType, (string)value);
				if (enumerationValue != null)
				{
					return enumerationValue;
				}
				return base.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<object> list = new List<object>();
			FieldInfo[] fields = _enumValueType.GetFields();
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				if (fieldInfo.IsStatic)
				{
					list.Add(fieldInfo.GetValue(fieldInfo.Name));
				}
			}
			return new StandardValuesCollection(list);
		}
	}
}
