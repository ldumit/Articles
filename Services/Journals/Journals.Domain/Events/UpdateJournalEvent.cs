using Articles.Domain;

namespace Journals.Domain.Events;


public record UpdateJournalEvent(
		int Id,
		string Abbreviation,
		string Name,
		string Description,
		string ISSN) : IDomainEvent;
