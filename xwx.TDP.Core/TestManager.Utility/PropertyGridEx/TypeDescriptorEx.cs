using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TestManager.Utility.PropertyGridEx
{
	public class TypeDescriptorEx : ICustomTypeDescriptor
	{
		private object _target;

		public object Target
		{
			get
			{
				return _target;
			}
		}

		public TypeDescriptorEx(object target)
		{
			_target = target;
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return _target;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(_target, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(_target, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(_target, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(_target, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(_target, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(_target, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(_target, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(_target, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(_target, attributes, true);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return GetProperties(null);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetExtendedPropertyDescriptor(_target, attributes);
		}

		public static PropertyDescriptorCollection GetExtendedPropertyDescriptor(object target, Attribute[] attributesFilter)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(target, attributesFilter, true);
			return GetExtendedPropertyDescriptors(properties);
		}

		internal static PropertyDescriptorCollection GetExtendedPropertyDescriptors(PropertyDescriptorCollection propertyDescriptors)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (PropertyDescriptor propertyDescriptor in propertyDescriptors)
			{
				Attribute[] extendedAttributes = GetExtendedAttributes(propertyDescriptor.PropertyType, propertyDescriptor.Attributes);
				list.Add(new PropertyDescriptorEx(propertyDescriptor, extendedAttributes));
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		internal static Attribute[] GetExtendedAttributes(Type type, AttributeCollection originalAttributes)
		{
			for (int i = 0; i < originalAttributes.Count; i++)
			{
				if (originalAttributes[i] is ValueLimitsAttribute)
				{
					(originalAttributes[i] as ValueLimitsAttribute).LimitValueType = type;
				}
			}
			List<Attribute> list = new List<Attribute>();
			bool flag = false;
			foreach (Attribute originalAttribute in originalAttributes)
			{
				Attribute item = originalAttribute;
				if (originalAttribute is TypeConverterAttribute)
				{
					flag = true;
				}
				list.Add(item);
			}
			if (!flag)
			{
				if (type.IsArray)
				{
					list.Add(new TypeConverterAttribute(typeof(DefaultArrayConverter)));
				}
				else if (type.GetInterface("ICollection") != null)
				{
					list.Add(new TypeConverterAttribute(typeof(DefaultCollcetionConverter)));
				}
				else if (type.IsEnum)
				{
					list.Add(new TypeConverterAttribute(typeof(EnumConverterEx)));
				}
				else if (type == typeof(bool))
				{
					list.Add(new TypeConverterAttribute(typeof(BooleanConverter)));
				}
				else if (type != typeof(Type))
				{
					list.Add(new TypeConverterAttribute(typeof(TypeConverterEx)));
				}
			}
			return list.ToArray();
		}

		internal static PropertyDescriptorEx GetExtendedPropertyDescriptor(PropertyDescriptor propertyDescriptor)
		{
			Attribute[] extendedAttributes = GetExtendedAttributes(propertyDescriptor.PropertyType, propertyDescriptor.Attributes);
			return new PropertyDescriptorEx(propertyDescriptor, extendedAttributes);
		}
	}
}
