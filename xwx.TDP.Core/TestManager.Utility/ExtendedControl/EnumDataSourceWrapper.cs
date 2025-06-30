using System;
using System.Collections.Generic;
using TestManager.Utility.PropertyGridEx;

namespace TestManager.Utility.ExtendedControl
{
	public class EnumDataSourceWrapper<T> : List<EnumDataSourceWrapper<T>.EnumWrapper>
	{
		public sealed class EnumWrapper
		{
			private T _value;

			public T Value
			{
				get
				{
					return _value;
				}
			}

			public string DisplayValue
			{
				get
				{
					return EnumDisplayNameAttribute.GetEnumerationDisplayName(_value as Enum);
				}
			}

			public EnumWrapper(T value)
			{
				if (!Enum.IsDefined(typeof(T), value))
				{
					throw new ArgumentException(string.Format("Value not defined in {1}: {0}", value, typeof(T).Name), "value");
				}
				_value = value;
			}
		}

		public EnumDataSourceWrapper()
		{
			if (!typeof(T).IsEnum)
			{
				throw new NotSupportedException("Only enum type is supported!");
			}
			foreach (T value in Enum.GetValues(typeof(T)))
			{
				Add(new EnumWrapper(value));
			}
		}
	}
}
