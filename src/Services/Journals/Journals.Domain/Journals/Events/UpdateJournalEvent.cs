using Blocks.Domain;

namespace Journals.Domain.Journals.Events;

public record UpdateJournalEvent(
		int Id,
		string Abbreviation,
		string Name,
		string Description,
		string ISSN) : IDomainEvent;
