using System.Text.Json;

namespace Production.Domain.ValueObjects;

public record AllowedFileExtensions
{
		private readonly HashSet<string> _extensions;

		public IReadOnlyList<string> Extensions => _extensions.ToList().AsReadOnly();

		public AllowedFileExtensions(IEnumerable<string> extensions)
		{
				_extensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);
		}

		public bool IsValidExtension(string extension)
		{
				return _extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
		}

		// Serialize the AllowedFileExtensions to JSON
		public string ToJson() => JsonSerializer.Serialize(_extensions);

		// Create an AllowedFileExtensions from JSON
		public static AllowedFileExtensions FromJson(string json)
		{
				var extensions = JsonSerializer.Deserialize<HashSet<string>>(json);
				return new AllowedFileExtensions(extensions ?? Enumerable.Empty<string>());
		}
}
