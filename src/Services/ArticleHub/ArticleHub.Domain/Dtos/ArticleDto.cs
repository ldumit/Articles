using Articles.Abstractions.Enums;

namespace ArticleHub.Domain.Dtos;

public class ArticleDto
{
		public int Id { get; init; }
		public string Title { get; init; }
		public string Doi { get; init; }
		public string Stage { get; init; }
		public DateTime SubmittedOn { get; init; }
		public DateTime? PublishedOn { get; init; }
		public DateTime? AcceptedOn { get; init; }
		public JournalDto Journal { get; init; }
		public PersonDto SubmittedBy { get; init; }
}

