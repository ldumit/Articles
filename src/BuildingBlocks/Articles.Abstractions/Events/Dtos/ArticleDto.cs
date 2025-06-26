using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record ArticleDto(
		int Id, 
		string Title,
		string Scope,
		string Doi,
		ArticleType Type,
		ArticleStage Stage, 
		JournalDto Journal,
		PersonDto SubmittedBy, 
		DateTime SubmittedOn, 
		DateTime? AcceptedOn, 
		DateTime? PublishedOn, 
		List<ActorDto> Actors
		);