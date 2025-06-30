using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TestManager.Utility.PropertyGridEx
{
	[TypeConverter(typeof(ValueLimitCollectionConverter))]
	public class ValueLimitCollection : List<ValueLimit>
	{
		public enum ValueValidResult
		{
			InLimit,
			NotInLimit,
			Error
		}

		private Type _globeValueType;

		public bool IndividualVauleTypeSpecified
		{
			get
			{
				for (int i = 0; i < base.Count; i++)
				{
					if (base[i].ValueType != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool IsValid
		{
			get
			{
				if (base.Count <= 0)
				{
					return false;
				}
				for (int i = 0; i < base.Count; i++)
				{
					if (!base[i].IsValid)
					{
						return false;
					}
				}
				return true;
			}
		}

		public Type GlobeValueType
		{
			get
			{
				if (base.Count == 1)
				{
					_globeValueType = base[0].ValueType;
				}
				else
				{
					for (int i = 0; i < base.Count - 1; i++)
					{
						if (base[i].ValueType != null && base[i + 1].ValueType != null && base[i].ValueType == base[i + 1].ValueType)
						{
							_globeValueType = base[i].ValueType;
						}
						else
						{
							_globeValueType = null;
						}
					}
				}
				return _globeValueType;
			}
			set
			{
				for (int i = 0; i < base.Count; i++)
				{
					base[i].ValueType = value;
				}
				_globeValueType = value;
			}
		}

		internal Type ValueType
		{
			set
			{
				for (int i = 0; i < base.Count; i++)
				{
					if (base[i].ValueType == null)
					{
						base[i].ValueType = value;
					}
				}
			}
		}

		public ValueLimitCollection()
		{
		}

		public ValueLimitCollection(string limitsString)
		{
			string[] array = limitsString.Trim().Split('$');
			if (array.Length >= 2 && array[1].Trim() != string.Empty)
			{
				array[1] = ValueLimit.ParseTypeString(array[1]);
				_globeValueType = Type.GetType(array[1], false, true);
			}
			string[] array2 = array[0].Split(';');
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array2[i].Trim();
				if (array2[i] != string.Empty)
				{
					ValueLimit valueLimit = ((_globeValueType != null) ? new ValueLimit(_globeValueType, array2[i]) : new ValueLimit(array2[i]));
					if (valueLimit.IsValid)
					{
						Add(valueLimit);
					}
				}
			}
		}

		internal ValueLimitCollection(string limitsString, Type globeValueType)
			: this((globeValueType == null) ? limitsString : string.Format("{0}${1}", limitsString, globeValueType.FullName))
		{
		}

		public object ValidateValue(object value)
		{
			object validatedValue = null;
			if (ValidateValue(value, out validatedValue) == ValueValidResult.InLimit)
			{
				return validatedValue;
			}
			return null;
		}

		public ValueValidResult ValidateValue(object valueToValid, out object validatedValue)
		{
			validatedValue = null;
			for (int i = 0; i < base.Count; i++)
			{
				ValueLimit valueLimit = base[i];
				switch (valueLimit.ValidateValue(valueToValid, out validatedValue))
				{
				case ValueLimit.ValueValidResult.InLimit:
					return ValueValidResult.InLimit;
				case ValueLimit.ValueValidResult.UnMatchedType:
				case ValueLimit.ValueValidResult.InvalidLimit:
				case ValueLimit.ValueValidResult.ValueNotComparable:
				case ValueLimit.ValueValidResult.NotInLimitValueUndetermine:
					return ValueValidResult.Error;
				}
			}
			return ValueValidResult.NotInLimit;
		}

		public override string ToString()
		{
			string text = string.Empty;
			for (int i = 0; i < base.Count; i++)
			{
				text += base[i].ToString();
				if (i < base.Count - 1)
				{
					text += "; ";
				}
			}
			return text;
		}

		public string ConvertToString()
		{
			string text = string.Empty;
			for (int i = 0; i < base.Count; i++)
			{
				text += base[i].ConvertToString();
				if (i < base.Count - 1)
				{
					text += "; ";
				}
			}
			return text;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(ValueLimitCollection))
			{
				return false;
			}
			ValueLimitCollection valueLimitCollection = obj as ValueLimitCollection;
			if (string.Compare(valueLimitCollection.ConvertToString(), ConvertToString(), true) != 0)
			{
				return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
