using Blocks.Domain;

namespace Journals.Domain.Events;

public record UpdateSectionEvent(
		int Id,
		string Name,
		string Description) : IDomainEvent;

