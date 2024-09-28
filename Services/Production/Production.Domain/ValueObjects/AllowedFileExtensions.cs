using System.Collections.Immutable;

namespace Production.Domain.ValueObjects;

public record AllowedFileExtensions
{
		public IReadOnlyList<string> Extensions { get; init; } = null!;

		public bool IsValidExtension(string extension) 
				=> Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
}
