using System;
using System.ComponentModel;

namespace TestManager.Utility.PropertyGridEx
{
	public class ExpandableObjectConverterEx : TypeConverterEx
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object target, Attribute[] attributes)
		{
			return TypeDescriptorEx.GetExtendedPropertyDescriptor(target, attributes);
		}
	}
}
