using System;
using System.ComponentModel;
using System.Reflection;

namespace TestManager.Utility.PropertyGridEx
{
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumDisplayNameAttribute : DisplayNameAttribute
	{
		public EnumDisplayNameAttribute(string displayName)
			: base(displayName)
		{
		}

		public static string GetEnumerationDisplayName(Enum value)
		{
			return GetEnumerationDisplayName(value.GetType(), value.ToString());
		}

		public static string GetEnumerationDisplayName(Type enumType, string name)
		{
			FieldInfo field = enumType.GetField(name);
			if (field != null)
			{
				EnumDisplayNameAttribute[] array = (EnumDisplayNameAttribute[])field.GetCustomAttributes(typeof(EnumDisplayNameAttribute), false);
				if (array.Length <= 0)
				{
					return name;
				}
				return array[0].DisplayName;
			}
			return string.Empty;
		}

		public static object GetEnumerationValue(Type enumType, string displayName)
		{
			FieldInfo[] fields = enumType.GetFields();
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				EnumDisplayNameAttribute[] array2 = (EnumDisplayNameAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDisplayNameAttribute), false);
				if (array2.Length == 0)
				{
					if (fieldInfo.Name == displayName)
					{
						return fieldInfo.GetValue(fieldInfo.Name);
					}
					continue;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j].DisplayName == displayName)
					{
						return fieldInfo.GetValue(fieldInfo.Name);
					}
				}
			}
			return null;
		}
	}
}
