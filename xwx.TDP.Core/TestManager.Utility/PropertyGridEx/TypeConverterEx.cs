using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace TestManager.Utility.PropertyGridEx
{
	public class TypeConverterEx : TypeConverter
	{
		private object _oldValue = 0;

		private TypeConverter _propertyConverter;

		public static TypeConverter GetDefinedTypeConverter(Type srcType)
		{
			if (srcType == null)
			{
				return null;
			}
			AttributeCollection attributes = TypeDescriptor.GetAttributes(srcType);
			TypeConverterAttribute typeConverterAttribute = null;
			TypeConverter result = null;
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is TypeConverterAttribute)
				{
					typeConverterAttribute = attributes[i] as TypeConverterAttribute;
					break;
				}
			}
			if (typeConverterAttribute != null)
			{
				Type type = Type.GetType(typeConverterAttribute.ConverterTypeName);
				if (type != null)
				{
					result = Activator.CreateInstance(type) as TypeConverter;
				}
			}
			return result;
		}

		public static object ChangeTypeEx(object value, Type desType)
		{
			if (value is string && (desType == typeof(short) || desType == typeof(int) || desType == typeof(long) || desType == typeof(ushort) || desType == typeof(uint) || desType == typeof(ulong)))
			{
				return Convert.ChangeType(Convert.ChangeType(value, typeof(double)), desType);
			}
			if (value is string && desType.IsEnum)
			{
				return Enum.Parse(desType, value as string);
			}
			return Convert.ChangeType(value, desType);
		}

		public static object ChangeStrongType(object value, Type desType)
		{
			if (value == null || desType == null || desType == Type.Missing)
			{
				return value;
			}
			if (desType == typeof(ValueLimit))
			{
				ValueLimitConverter valueLimitConverter = new ValueLimitConverter();
				if (valueLimitConverter.CanConvertFrom(value.GetType()))
				{
					return valueLimitConverter.ConvertFrom(value);
				}
				return null;
			}
			if (value.GetType() == typeof(ValueLimit))
			{
				ValueLimitConverter valueLimitConverter2 = new ValueLimitConverter();
				if (valueLimitConverter2.CanConvertTo(desType))
				{
					if (desType == typeof(string))
					{
						return (value as ValueLimit).ConvertToString();
					}
					return valueLimitConverter2.ConvertTo(value, desType);
				}
				return null;
			}
			if (desType == typeof(ValueLimitCollection))
			{
				ValueLimitCollectionConverter valueLimitCollectionConverter = new ValueLimitCollectionConverter();
				if (valueLimitCollectionConverter.CanConvertFrom(value.GetType()))
				{
					return valueLimitCollectionConverter.ConvertFrom(value);
				}
				return null;
			}
			if (value.GetType() == typeof(ValueLimitCollection))
			{
				ValueLimitCollectionConverter valueLimitCollectionConverter2 = new ValueLimitCollectionConverter();
				if (valueLimitCollectionConverter2.CanConvertTo(desType))
				{
					if (desType == typeof(string))
					{
						return (value as ValueLimitCollection).ConvertToString();
					}
					return valueLimitCollectionConverter2.ConvertTo(value, desType);
				}
				return null;
			}
			if (value is string && desType.IsArray)
			{
				return DefaultArrayConverter.ConvertFromString(value as string, desType);
			}
			if (value.GetType().IsArray && desType == typeof(string))
			{
				return DefaultArrayConverter.ConvertToString(value as Array);
			}
			if (value is string && desType.GetInterface("ICollection") != null)
			{
				return DefaultCollcetionConverter.ConvertFromString(value as string, desType);
			}
			if (value.GetType().GetInterface("ICollection") != null && desType == typeof(string))
			{
				return DefaultCollcetionConverter.ConvertToString(value as ICollection);
			}
			try
			{
				return ChangeTypeEx(value, desType);
			}
			catch
			{
				TypeConverter definedTypeConverter = GetDefinedTypeConverter(value.GetType());
				TypeConverter definedTypeConverter2 = GetDefinedTypeConverter(desType);
				if (definedTypeConverter == null && definedTypeConverter2 == null)
				{
					return null;
				}
				if (definedTypeConverter != null && definedTypeConverter.CanConvertTo(desType))
				{
					return definedTypeConverter.ConvertTo(value, desType);
				}
				if (definedTypeConverter2 != null && definedTypeConverter2.CanConvertFrom(value.GetType()))
				{
					return definedTypeConverter2.ConvertFrom(value);
				}
			}
			return null;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (_propertyConverter == null && context != null && context.PropertyDescriptor != null)
			{
				_propertyConverter = GetDefinedTypeConverter(context.PropertyDescriptor.PropertyType);
			}
			if (_propertyConverter != null)
			{
				return _propertyConverter.CanConvertTo(context, destinationType);
			}
			try
			{
				object obj = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);
				if (obj != null && ChangeTypeEx(obj, destinationType) != null)
				{
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (_propertyConverter == null && context != null && context.PropertyDescriptor != null)
			{
				_propertyConverter = GetDefinedTypeConverter(context.PropertyDescriptor.PropertyType);
			}
			if (sourceType == typeof(string))
			{
				return true;
			}
			if (_propertyConverter != null)
			{
				return _propertyConverter.CanConvertFrom(context, sourceType);
			}
			try
			{
				object obj = Activator.CreateInstance(sourceType);
				if (obj != null && ChangeTypeEx(obj, context.PropertyDescriptor.PropertyType) != null)
				{
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			_oldValue = value;
			if (_propertyConverter != null)
			{
				try
				{
					return _propertyConverter.ConvertTo(context, culture, value, destinationType);
				}
				catch
				{
					return null;
				}
			}
			try
			{
				return ChangeTypeEx(value, destinationType);
			}
			catch
			{
				return null;
			}
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			object obj = null;
			if (_propertyConverter != null)
			{
				try
				{
					obj = _propertyConverter.ConvertFrom(context, culture, value);
				}
				catch
				{
					return _oldValue;
				}
			}
			else
			{
				try
				{
					obj = ChangeTypeEx(value, _oldValue.GetType());
				}
				catch
				{
					return _oldValue;
				}
			}
			if (obj == null)
			{
				return _oldValue;
			}
			if (context == null || context.PropertyDescriptor == null)
			{
				return obj;
			}
			ValueLimitsAttribute valueLimitsAttribute = null;
			for (int i = 0; i < context.PropertyDescriptor.Attributes.Count; i++)
			{
				if (context.PropertyDescriptor.Attributes[i] is ValueLimitsAttribute)
				{
					valueLimitsAttribute = context.PropertyDescriptor.Attributes[i] as ValueLimitsAttribute;
				}
			}
			if (valueLimitsAttribute != null)
			{
				object obj4 = valueLimitsAttribute.ValidateValue(obj);
				if (obj4 != null)
				{
					return obj4;
				}
				return _oldValue;
			}
			return obj;
		}
	}
}
