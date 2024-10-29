using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public partial class Author : Person
{
		public string? Affiliation { get; set; }
}
