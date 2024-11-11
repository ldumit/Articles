using Articles.Abstractions.Enums;

namespace Submission.Application.Dtos;

public record ArticleDto(
		int id,
		string Title,
		string Scope,
		ArticleStage Stage, 
		JournalDto Journal,
		DateTime? SubmitedOn,
		PersonDto? SubmitedBy,
		List<ActorDto> Actors,
		List<AssetDto> Assets
);