using System.Collections.Immutable;

namespace Production.Domain.ValueObjects;

public class AllowedFileExtensions
{
		public IReadOnlyList<string> Extensions { get; init; } = null!;

		public bool IsValidExtension(string extension)
		{
				return Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
		}
}
