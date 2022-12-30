using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class EnumExtension
{
	public static TEnum GetLastValue<TEnum>()
	{
		var values = Enum.GetValues(typeof(TEnum));
		var last = values.GetValue(values.Length - 1);
		return (TEnum)last;
	}

	public static int GetLenght<TEnum>()
	{
		int count = Enum.GetValues(typeof(TEnum)).Length;
		return count;
	}

	public static string ToDescription(this Enum value)
	{
		FieldInfo fi = value.GetType().GetField(value.ToString());

		DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

		if (attributes != null && attributes.Any())
		{
			return attributes.First().Description;
		}

		return value.ToString();
	}
}
