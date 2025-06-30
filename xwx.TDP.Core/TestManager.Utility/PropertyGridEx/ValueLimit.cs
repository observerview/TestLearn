using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TestManager.Utility.PropertyGridEx
{
	[TypeConverter(typeof(ValueLimitConverter))]
	public class ValueLimit
	{
		public enum CompareType
		{
			EQU,
			None,
			LT,
			LE,
			GT,
			GE,
			GTLT,
			GELT,
			GTLE,
			GELE
		}

		public enum ValueValidResult
		{
			UnMatchedType,
			InvalidLimit,
			ValueNotComparable,
			InLimit,
			NotInLimit,
			NotInLimitValueUndetermine
		}

		private List<object> _valueSerial;

		private object _downLmt;

		private object _upLmt;

		private CompareType _compareType;

		private Type _valueType;

		private bool _isValid = true;

		[Category("Limit Parameters")]
		[Description("The value serials.")]
		[DisplayName("Value Serial")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public string ValueSerial
		{
			get
			{
				if (_valueSerial == null)
				{
					return string.Empty;
				}
				string text = "{";
				for (int i = 0; i < _valueSerial.Count; i++)
				{
					text += _valueSerial[i].ToString();
					if (i < _valueSerial.Count - 1)
					{
						text += ",";
					}
				}
				return text + "}";
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				string[] array = value.Trim().Split('|');
				if (array.Length >= 2 && array[1].Trim() != string.Empty)
				{
					array[1] = ParseTypeString(array[1]);
					_valueType = Type.GetType(array[1], false, true);
				}
				array = array[0].Trim().Trim('{', '}').Split(',');
				List<string> list = new List<string>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
					if (array[i] != string.Empty)
					{
						list.Add(array[i]);
					}
				}
				if (list.Count == 0)
				{
					_valueSerial = new List<object>();
					return;
				}
				List<object> list2 = new List<object>();
				for (int j = 0; j < list.Count; j++)
				{
					if (_valueType != null)
					{
						object obj = TypeConverterEx.ChangeStrongType(list[j], _valueType);
						if (obj == null)
						{
							_isValid = false;
							return;
						}
						list2.Add(obj);
					}
					else
					{
						list2.Add(list[j]);
					}
				}
				_compareType = CompareType.EQU;
				UpLmt = null;
				DownLmt = null;
				_valueSerial = list2;
			}
		}

		[DisplayName("Down Limit")]
		[TypeConverter(typeof(TypeConverterEx))]
		[Description("The down limit.")]
		[Category("Limit Parameters")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public object DownLmt
		{
			get
			{
				return _downLmt;
			}
			set
			{
				if (_valueType == null || value == null)
				{
					_downLmt = value;
					return;
				}
				object obj = TypeConverterEx.ChangeStrongType(value, _valueType);
				if (obj != null)
				{
					_downLmt = obj;
					ValueSerial = string.Empty;
				}
				else
				{
					_downLmt = null;
					_isValid = false;
				}
			}
		}

		[Description("The up limit.")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(TypeConverterEx))]
		[Category("Limit Parameters")]
		[DisplayName("Up Limit")]
		public object UpLmt
		{
			get
			{
				return _upLmt;
			}
			set
			{
				if (_valueType == null || value == null)
				{
					_upLmt = value;
					return;
				}
				object obj = TypeConverterEx.ChangeStrongType(value, _valueType);
				if (obj != null)
				{
					_upLmt = obj;
					ValueSerial = string.Empty;
				}
				else
				{
					_upLmt = null;
					_isValid = false;
				}
			}
		}

		[RefreshProperties(RefreshProperties.Repaint)]
		[DisplayName("Limit Type")]
		[Category("Limit Parameters")]
		[Description("The limit comparison type.")]
		public CompareType LimitType
		{
			get
			{
				return _compareType;
			}
			set
			{
				_compareType = value;
				switch (_compareType)
				{
				case CompareType.None:
					DownLmt = null;
					UpLmt = null;
					ValueSerial = string.Empty;
					break;
				case CompareType.EQU:
					DownLmt = null;
					UpLmt = null;
					if (string.IsNullOrEmpty(ValueSerial))
					{
						ValueSerial = "{}";
					}
					break;
				case CompareType.GT:
				case CompareType.GE:
					if (DownLmt == null)
					{
						DownLmt = 0;
					}
					UpLmt = null;
					ValueSerial = string.Empty;
					break;
				case CompareType.GTLT:
				case CompareType.GELT:
				case CompareType.GTLE:
				case CompareType.GELE:
					if (DownLmt == null)
					{
						DownLmt = 0;
					}
					if (UpLmt == null)
					{
						UpLmt = 0;
					}
					ValueSerial = string.Empty;
					break;
				case CompareType.LT:
				case CompareType.LE:
					DownLmt = null;
					if (UpLmt == null)
					{
						UpLmt = 0;
					}
					ValueSerial = string.Empty;
					break;
				}
			}
		}

		[DisplayName("Value Type")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[ReadOnly(true)]
		[Description("The type of the limit value.")]
		[Category("Limit Parameters")]
		public Type ValueType
		{
			get
			{
				return _valueType;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				object obj = null;
				object obj2 = null;
				List<object> list = new List<object>();
				if (_downLmt != null)
				{
					obj = TypeConverterEx.ChangeStrongType(_downLmt, value);
					if (obj == null)
					{
						_isValid = false;
						return;
					}
				}
				if (_upLmt != null)
				{
					obj2 = TypeConverterEx.ChangeStrongType(_upLmt, value);
					if (obj2 == null)
					{
						_isValid = false;
						return;
					}
				}
				if (_valueSerial != null)
				{
					for (int i = 0; i < _valueSerial.Count; i++)
					{
						object obj3 = TypeConverterEx.ChangeStrongType(_valueSerial[i], value);
						if (obj3 == null)
						{
							_isValid = false;
							return;
						}
						list.Add(obj3);
					}
				}
				_downLmt = obj;
				_upLmt = obj2;
				_valueSerial = list;
				_valueType = value;
			}
		}

		[DisplayName("Is Valid")]
		[Browsable(false)]
		[ReadOnly(true)]
		[Category("Limit Parameters")]
		public bool IsValid
		{
			get
			{
				if (!_isValid)
				{
					return false;
				}
				if (_compareType == CompareType.EQU)
				{
					if (_valueSerial == null)
					{
						return false;
					}
					for (int i = 0; i < _valueSerial.Count; i++)
					{
						if (!(_valueSerial[i] is IComparable))
						{
							return false;
						}
					}
					return true;
				}
				if (_downLmt == null && _upLmt == null)
				{
					if (_compareType != CompareType.None)
					{
						return false;
					}
					return true;
				}
				if (_downLmt != null && _upLmt != null)
				{
					if (_downLmt is IComparable && _upLmt is IComparable)
					{
						int num = (_downLmt as IComparable).CompareTo(_upLmt);
						if (num > 0 || (num == 0 && _compareType != CompareType.GELE))
						{
							return false;
						}
						return true;
					}
					return true;
				}
				if (_downLmt != null)
				{
					ValueType = _downLmt.GetType();
				}
				if (_upLmt != null)
				{
					ValueType = _upLmt.GetType();
				}
				if (_downLmt == null)
				{
					switch (_compareType)
					{
					case CompareType.GT:
					case CompareType.GE:
					case CompareType.GTLT:
					case CompareType.GELT:
					case CompareType.GTLE:
					case CompareType.GELE:
						return false;
					}
				}
				else if (!(_downLmt is IComparable))
				{
					return false;
				}
				if (_upLmt == null)
				{
					switch (_compareType)
					{
					case CompareType.EQU:
					case CompareType.None:
					case CompareType.LT:
					case CompareType.LE:
					case CompareType.GTLT:
					case CompareType.GELT:
					case CompareType.GTLE:
					case CompareType.GELE:
						return false;
					}
				}
				else if (!(_upLmt is IComparable))
				{
					return false;
				}
				return true;
			}
		}

		public static string ParseTypeString(string originalTypeString)
		{
			switch (originalTypeString.Trim().ToLower())
			{
			case "bool":
				return "System.Boolean";
			case "byte":
				return "System.Byte";
			case "char":
				return "System.Char";
			case "time":
				return "System.DateTime";
			case "decimal":
				return "System.Decimal";
			case "double":
				return "System.Double";
			case "short":
			case "int16":
				return "System.Int16";
			case "int":
			case "int32":
				return "System.Int32";
			case "long":
			case "int64":
				return "System.Int64";
			case "ubyte":
			case "sbyte":
				return "System.Sbyte";
			case "float":
			case "single":
				return "System.Single";
			case "string":
				return "System.String";
			case "ushort":
			case "uint16":
				return "System.UInt16";
			case "uint":
			case "uint32":
				return "System.UInt32";
			case "ulong":
			case "uint64":
				return "System.UInt64";
			default:
				return originalTypeString;
			}
		}

		internal ValueLimit(object downLmt, object upLmt, CompareType compareType)
			: this(null, downLmt, upLmt, compareType)
		{
		}

		public ValueLimit(Type valueType, object downLmt, object upLmt, CompareType compareType)
		{
			_valueType = valueType;
			_compareType = compareType;
			DownLmt = downLmt;
			UpLmt = upLmt;
		}

		public ValueLimit(string limitString)
		{
			string[] array = limitString.Trim().Split('|');
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Trim() != string.Empty)
				{
					list.Add(array[i].Trim());
				}
			}
			if (list.Count != 0)
			{
				Type valueType = null;
				if (list.Count >= 2)
				{
					list[1] = ParseTypeString(list[1]);
					valueType = Type.GetType(list[1], false, true);
				}
				ValueLimitFromString(valueType, list[0]);
			}
		}

		public ValueLimit()
			: this(typeof(double), 0, 0, CompareType.GELE)
		{
		}

		public ValueLimit(Type valueType, string limitString)
		{
			ValueLimitFromString(valueType, limitString);
		}

		private void ValueLimitFromString(Type valueType, string limitString)
		{
			_valueType = valueType;
			string text = limitString.Trim();
			if ((text.StartsWith("(") && text.EndsWith(")")) || (text.StartsWith("(") && text.EndsWith("]")) || (text.StartsWith("[") && text.EndsWith(")")) || (text.StartsWith("[") && text.EndsWith("]")))
			{
				string text2 = text.Substring(1, text.Length - 2).Trim();
				string[] array = text2.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				if (array.Length < 2)
				{
					_isValid = false;
					return;
				}
				if (string.Compare(array[0], "null", true) == 0 || string.Compare(array[0], "-Inf", true) == 0 || string.Compare(array[0], "Inf", true) == 0)
				{
					text = "(" + text.Substring(1);
				}
				if (string.Compare(array[1], "null", true) == 0 || string.Compare(array[1], "+Inf", true) == 0 || string.Compare(array[1], "Inf", true) == 0)
				{
					text = text.Substring(0, text.Length - 1) + ")";
				}
				if (text.StartsWith("(") && text.EndsWith(")"))
				{
					if ((string.Compare(array[0], "null", true) == 0 || string.Compare(array[0], "-Inf", true) == 0 || string.Compare(array[0], "Inf", true) == 0) && (string.Compare(array[1], "null", true) == 0 || string.Compare(array[1], "+Inf", true) == 0 || string.Compare(array[1], "Inf", true) == 0))
					{
						_compareType = CompareType.None;
					}
					else if (string.Compare(array[0], "null", true) == 0 || string.Compare(array[0], "-Inf", true) == 0 || string.Compare(array[0], "Inf", true) == 0)
					{
						_compareType = CompareType.LT;
						UpLmt = array[1];
					}
					else if (string.Compare(array[1], "null", true) == 0 || string.Compare(array[1], "+Inf", true) == 0 || string.Compare(array[1], "Inf", true) == 0)
					{
						_compareType = CompareType.GT;
						DownLmt = array[0];
					}
					else
					{
						_compareType = CompareType.GTLT;
						DownLmt = array[0];
						UpLmt = array[1];
					}
				}
				else if (text.StartsWith("(") && text.EndsWith("]"))
				{
					if (string.Compare(array[0], "null", true) == 0 || string.Compare(array[0], "-Inf", true) == 0 || string.Compare(array[0], "Inf", true) == 0)
					{
						_compareType = CompareType.LE;
						UpLmt = array[1];
					}
					else
					{
						_compareType = CompareType.GTLE;
						DownLmt = array[0];
						UpLmt = array[1];
					}
				}
				else if (text.StartsWith("[") && text.EndsWith(")"))
				{
					if (string.Compare(array[1], "null", true) == 0 || string.Compare(array[1], "+Inf", true) == 0 || string.Compare(array[1], "Inf", true) == 0)
					{
						_compareType = CompareType.GE;
						DownLmt = array[0];
					}
					else
					{
						_compareType = CompareType.GELT;
						DownLmt = array[0];
						UpLmt = array[1];
					}
				}
				else if (text.StartsWith("[") && text.EndsWith("]"))
				{
					_compareType = CompareType.GELE;
					DownLmt = array[0];
					UpLmt = array[1];
				}
			}
			else
			{
				ValueSerial = text;
			}
		}

		public ValueValidResult ValidateValue(object valueToValid, out object validatedValue)
		{
			if (_compareType == CompareType.None)
			{
				validatedValue = valueToValid;
				return ValueValidResult.InLimit;
			}
			validatedValue = null;
			if (!IsValid)
			{
				return ValueValidResult.InvalidLimit;
			}
			if (_valueType == null)
			{
				Type type = null;
				Type type2 = null;
				if (_downLmt != null)
				{
					type = _downLmt.GetType();
				}
				if (UpLmt != null)
				{
					type2 = _upLmt.GetType();
				}
				if (UpLmt != null && _downLmt != null && type != type2)
				{
					return ValueValidResult.InvalidLimit;
				}
				if (type != null)
				{
					ValueType = type;
				}
				if (type2 != null)
				{
					ValueType = type2;
				}
			}
			valueToValid = TypeConverterEx.ChangeStrongType(valueToValid, _valueType);
			if (valueToValid != null)
			{
				validatedValue = valueToValid;
				if (!(valueToValid is IComparable))
				{
					return ValueValidResult.ValueNotComparable;
				}
				switch (_compareType)
				{
				case CompareType.None:
					return ValueValidResult.InLimit;
				case CompareType.EQU:
					if (!_valueSerial.Contains(valueToValid))
					{
						return ValueValidResult.NotInLimit;
					}
					return ValueValidResult.InLimit;
				case CompareType.GT:
					if ((valueToValid as IComparable).CompareTo(_downLmt) > 0)
					{
						return ValueValidResult.InLimit;
					}
					return ValueValidResult.NotInLimitValueUndetermine;
				case CompareType.GE:
					if ((valueToValid as IComparable).CompareTo(_downLmt) >= 0)
					{
						return ValueValidResult.InLimit;
					}
					validatedValue = _upLmt;
					return ValueValidResult.NotInLimit;
				case CompareType.GTLT:
					if ((valueToValid as IComparable).CompareTo(_upLmt) >= 0)
					{
						return ValueValidResult.NotInLimitValueUndetermine;
					}
					if ((valueToValid as IComparable).CompareTo(_downLmt) <= 0)
					{
						return ValueValidResult.NotInLimitValueUndetermine;
					}
					return ValueValidResult.InLimit;
				case CompareType.GELT:
					if ((valueToValid as IComparable).CompareTo(_upLmt) >= 0)
					{
						return ValueValidResult.NotInLimitValueUndetermine;
					}
					if ((valueToValid as IComparable).CompareTo(_downLmt) < 0)
					{
						validatedValue = _downLmt;
						return ValueValidResult.NotInLimit;
					}
					return ValueValidResult.InLimit;
				case CompareType.GTLE:
					if ((valueToValid as IComparable).CompareTo(_upLmt) > 0)
					{
						validatedValue = _upLmt;
						return ValueValidResult.NotInLimit;
					}
					if ((valueToValid as IComparable).CompareTo(_downLmt) <= 0)
					{
						return ValueValidResult.NotInLimitValueUndetermine;
					}
					return ValueValidResult.InLimit;
				case CompareType.GELE:
					if ((valueToValid as IComparable).CompareTo(_upLmt) > 0)
					{
						validatedValue = _upLmt;
						return ValueValidResult.NotInLimit;
					}
					if ((valueToValid as IComparable).CompareTo(_downLmt) < 0)
					{
						validatedValue = _downLmt;
						return ValueValidResult.NotInLimit;
					}
					return ValueValidResult.InLimit;
				case CompareType.LT:
					if ((valueToValid as IComparable).CompareTo(_upLmt) < 0)
					{
						return ValueValidResult.InLimit;
					}
					return ValueValidResult.NotInLimitValueUndetermine;
				case CompareType.LE:
					if ((valueToValid as IComparable).CompareTo(_upLmt) <= 0)
					{
						return ValueValidResult.InLimit;
					}
					validatedValue = _downLmt;
					return ValueValidResult.NotInLimit;
				default:
					return ValueValidResult.NotInLimit;
				}
			}
			return ValueValidResult.UnMatchedType;
		}

		public string ConvertToString()
		{
			return ToString() + " | " + ValueType.ToString();
		}

		public override string ToString()
		{
			switch (_compareType)
			{
			case CompareType.None:
				return "(-Inf,+Inf)";
			case CompareType.EQU:
				return ValueSerial;
			case CompareType.GT:
				return "(" + _downLmt.ToString() + ",+Inf)";
			case CompareType.GE:
				return "[" + _downLmt.ToString() + ",+Inf)";
			case CompareType.GTLT:
				return "(" + _downLmt.ToString() + "," + _upLmt.ToString() + ")";
			case CompareType.GELT:
				return "[" + _downLmt.ToString() + "," + _upLmt.ToString() + ")";
			case CompareType.GTLE:
				return "(" + _downLmt.ToString() + "," + _upLmt.ToString() + "]";
			case CompareType.GELE:
				return "[" + _downLmt.ToString() + "," + _upLmt.ToString() + "]";
			case CompareType.LT:
				return "(-Inf," + _upLmt.ToString() + ")";
			case CompareType.LE:
				return "(-Inf," + _upLmt.ToString() + "]";
			default:
				return string.Empty;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(ValueLimit))
			{
				return false;
			}
			ValueLimit valueLimit = obj as ValueLimit;
			if (string.Compare(valueLimit.ConvertToString(), ConvertToString(), true) != 0)
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
