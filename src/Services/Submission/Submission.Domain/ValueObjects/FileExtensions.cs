using Articles.Entitities;
using Articles.System;
using System.Collections.Immutable;

namespace Submission.Domain.ValueObjects;

public class FileExtensions : IValueObject
{
		public IReadOnlyList<string> Extensions { get; init; } = null!;

		public bool IsValidExtension(string extension)
				// if the list is empty, then all extensions are allowed
				=> Extensions.IsEmpty() || Extensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
}
