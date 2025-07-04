using Blocks.Exceptions;

namespace Blocks.Core;

public static class GuardExtensions
{
		public static T OrThrowNotFound<T>(this T? value, string? message = null) where T : class
				=> value ?? throw new NotFoundException(message ?? $"{typeof(T).Name} not found");
}
