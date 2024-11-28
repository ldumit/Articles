using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record ArticleDto(
		int Id, 
		string Title, 
		string Doi, 
		ArticleStage Stage, 
		int SubmitedById, 
		PersonDto SubmitedBy, 
		DateTime SubmitedOn, 
		DateTime? AcceptedOn, 
		DateTime? PublishedOn, 
		int JournalId, 
		JournalDto Journal, 
		IReadOnlyCollection<ArticleContributor> Contributors
		);