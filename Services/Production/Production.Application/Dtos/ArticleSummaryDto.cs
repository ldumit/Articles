using Articles.Abstractions;

namespace Production.Application.Dtos;

public record ArticleSummaryDto(
		int id,
		string Title, 
		string Doi, 
		ArticleStage Stage, 
		JournalDto Journal,
		int VolumeId,
		DateTime SubmitedOn,
		PersonDto SubmitedBy,
		DateTime PublishedOn,
		PersonDto PublishedBy,
		List<ActorDto> Actors
);