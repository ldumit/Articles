namespace Articles.System;

public static class StringExtensions
{
	public static bool LacksValue(this string text) => string.IsNullOrEmpty(text);

	public static string FormatWith(this string @this, object arg, params object[] additionalArgs)
	{
		try
		{
			if (additionalArgs == null || additionalArgs.Length == 0)
				return string.Format(@this, arg);
			else
				return string.Format(@this, new object[] { arg }.Concat(additionalArgs).ToArray());
		}
		catch (Exception ex)
		{
			throw new FormatException("Cannot format the string '{0}' with the specified arguments.".FormatWith(@this), ex);
		}
	}

	public static bool HasValue(this string str) => !HasNoValue(str);

	public static bool HasNoValue(this string str) => string.IsNullOrWhiteSpace(str);

	public static string WithPrefix(this string @this, string prefix)
	{
		if (@this.LacksValue()) return string.Empty;
		else return prefix + @this;
	}

	public static string WithSuffix(this string @this, string suffix)
	{
		if (@this.LacksValue()) return string.Empty;
		else return @this + suffix;
	}

	public static string OrEmpty(this string @this) => @this ?? string.Empty;

	public static string TrimOrNull(this string @this) => @this?.Trim();

	public static string TrimOrEmpty(this string @this) => @this.TrimOrNull().OrEmpty();

    public static string TrimAfter(this string @this, string phrase, bool trimPhrase = true, bool caseSensitive = false)
    {
        if (@this.LacksValue()) return @this;

        int index;

        if (caseSensitive) index = @this.IndexOf(phrase);
        else
            index = @this.IndexOf(phrase, StringComparison.OrdinalIgnoreCase);

        if (index == -1) return @this;

        if (!trimPhrase) index += phrase.Length;

        return @this.Substring(0, index);
    }

    public static string[] ToLines(this string @this)
	{
		if (@this is null) return new string[0];

		return @this.Split('\n').Select(l => l.Trim('\r')).ToArray();
	}

	public static bool IsNullOrEmpty(this string value)
	{
		return string.IsNullOrEmpty(value);
	}

	public static bool EqualsIgnoreCase(this string me, string theOther)
	{
		return me.Equals(theOther, StringComparison.InvariantCultureIgnoreCase);
	}
	public static bool EqualsOrdinalIgnoreCase(this string first, string second)
	{
		return string.Compare(first, second, StringComparison.OrdinalIgnoreCase) == 0;
	}

    public static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static string ToLowerCamelCase(this string me)
	{
            if (!string.IsNullOrEmpty(me))
            {
                return char.ToLowerInvariant(me[0]) + me.Substring(1);
            }

            return me;
        }

	public static string FirstCharToUpper(this string input)
	{
		switch (input)
		{
			case null: throw new ArgumentNullException(nameof(input));
			case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
			default: return input.First().ToString().ToUpper() + input.Substring(1);
		}
	}

	public static T ToEnum<T>(this string value)
	{
		return (T)Enum.Parse(typeof(T), value, true);
	}
    
	public static string FormatWith(this string value, params object[] args) => String.Format(value, args);
}

