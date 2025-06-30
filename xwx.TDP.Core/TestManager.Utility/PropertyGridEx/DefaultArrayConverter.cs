using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TestManager.Utility.PropertyGridEx
{
	public class DefaultArrayConverter : ArrayConverter
	{
		private static bool IsBaseValueType(Type srcValueType)
		{
			if (srcValueType == null)
			{
				return false;
			}
			if (srcValueType.IsEnum)
			{
				return true;
			}
			switch (srcValueType.FullName)
			{
			case "System.Boolean":
			case "System.Byte":
			case "System.Char":
			case "System.Decimal":
			case "System.Double":
			case "System.Int16":
			case "System.Int32":
			case "System.Int64":
			case "System.Sbyte":
			case "System.Single":
			case "System.UInt16":
			case "System.UInt32":
			case "System.UInt64":
				return true;
			default:
				return false;
			}
		}

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
			if (destinationType == typeof(string) && value.GetType().IsArray)
			{
				return ConvertToString(value as Array);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string && context != null && context.PropertyDescriptor != null && context.PropertyDescriptor.PropertyType.IsArray)
			{
				return ConvertFromString(value as string, context.PropertyDescriptor.PropertyType);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public static string ConvertToString(Array value)
		{
			Type srcValueType = null;
			Regex regex = new Regex("\\[\\]");
			Match match = regex.Match(value.GetType().FullName);
			if (match.Success)
			{
				string typeName = value.GetType().FullName.Remove(match.Index);
				srcValueType = Type.GetType(typeName, false);
			}
			string text = (IsBaseValueType(srcValueType) ? "," : "; ");
			IEnumerator enumerator = value.GetEnumerator();
			string text2 = string.Empty;
			while (enumerator.MoveNext())
			{
				object obj = TypeConverterEx.ChangeStrongType(enumerator.Current, typeof(string));
				if (obj != null && obj is string)
				{
					text2 = text2 + (obj as string) + text;
				}
			}
			if (text2 != string.Empty)
			{
				text2 = text2.Remove(text2.Length - text.Length);
			}
			if (!(text == ","))
			{
				return "{" + text2 + "}";
			}
			return text2;
		}

		public static Array ConvertFromString(string value, Type arrayType)
		{
			Type type = null;
			Regex regex = new Regex("\\[\\]");
			Match match = regex.Match(arrayType.FullName);
			if (match.Success)
			{
				string typeName = arrayType.FullName.Remove(match.Index);
				type = Type.GetType(typeName, false);
			}
			string text = (IsBaseValueType(type) ? ",;" : ";");
			ArrayList arrayList = new ArrayList();
			string[] array = value.Trim().Trim('{', '}').Split(text.ToCharArray());
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim();
				if (text2 != string.Empty)
				{
					object obj = TypeConverterEx.ChangeStrongType(text2, type);
					if (obj != null)
					{
						arrayList.Add(obj);
					}
				}
			}
			return arrayList.ToArray(type);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return base.GetProperties(context, value, attributes);
		}
	}
}
