namespace Articles.System;

public static class Guard
{
		//todo use Guard instead of ArgumentException, add more Guards, use name parameter 
		public static void ThrowIfNull<T>(T? value, string name)
		{
				if (value is null)
				{
						throw new ArgumentNullException(name);
				}
		}

		public static void ThrowIfNullOrWhiteSpace(string value)
				=> ArgumentException.ThrowIfNullOrWhiteSpace(value);

		public static void ThrowIfFalse(this bool condition, string message = "Condition must be true.")
		{
				if (!condition)
						throw new ArgumentException(message);
		}
}
