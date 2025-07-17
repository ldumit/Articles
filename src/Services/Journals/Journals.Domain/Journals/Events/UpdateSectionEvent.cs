using Blocks.Domain;

namespace Journals.Domain.Journals.Events;

public record UpdateSectionEvent(
		int Id,
		string Name,
		string Description) : IDomainEvent;

