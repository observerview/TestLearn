using System;

namespace TestManager.Utility.PropertyGridEx
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ValueLimitsAttribute : Attribute
	{
		private ValueLimitCollection _valueLimits = new ValueLimitCollection();

		public static readonly ValueLimitsAttribute Default = new ValueLimitsAttribute();

		public Type LimitValueType
		{
			set
			{
				_valueLimits.GlobeValueType = value;
			}
		}

		public ValueLimitsAttribute(params string[] limits)
		{
			foreach (string limitString in limits)
			{
				ValueLimit valueLimit = new ValueLimit(limitString);
				if (valueLimit.IsValid)
				{
					_valueLimits.Add(valueLimit);
				}
			}
		}

		public ValueLimitsAttribute(ValueLimit.CompareType compareType, object downLmt, object upLmt)
		{
			_valueLimits.Add(new ValueLimit(downLmt, upLmt, compareType));
		}

		public object ValidateValue(object value)
		{
			return _valueLimits.ValidateValue(value);
		}

		public override string ToString()
		{
			if (_valueLimits != null)
			{
				return _valueLimits.ToString();
			}
			return base.ToString();
		}
	}
}
