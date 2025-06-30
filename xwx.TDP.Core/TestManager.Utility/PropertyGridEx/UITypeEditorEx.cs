using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace TestManager.Utility.PropertyGridEx
{
	public class UITypeEditorEx : UITypeEditor
	{
		protected object ValidateValue(ITypeDescriptorContext context, object valueToValidate, object originalValue)
		{
			foreach (Attribute attribute in context.PropertyDescriptor.Attributes)
			{
				if (attribute is ValueLimitsAttribute)
				{
					object obj = (attribute as ValueLimitsAttribute).ValidateValue(valueToValidate);
					valueToValidate = ((obj == null) ? originalValue : obj);
					break;
				}
			}
			return valueToValidate;
		}
	}
}
