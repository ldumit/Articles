using Articles.Abstractions.Enums;

namespace ArticleHub.Domain.Dtos;

public record ArticleDto(
		int Id,
		string Title,
		string Doi,
		string Stage,
		DateTime SubmittedOn,
		DateTime? PublishedOn,
		DateTime? AcceptedOn,
		JournalDto Journal,
		PersonDto SubmittedBy
);

