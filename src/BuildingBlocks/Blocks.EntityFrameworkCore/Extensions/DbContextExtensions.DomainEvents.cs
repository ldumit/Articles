using Blocks.Domain;

namespace Blocks.EntityFrameworkCore;

public static partial class DbContextExtensions
{
		public static async Task<int> DispatchDomainEventsAsync(this DbContext ctx, IDomainEventPublisher eventPublisher, CancellationToken ct = default)
		{
				var domainEntities = ctx.ChangeTracker
						.Entries<IAggregateRoot>();

				var domainEvents = domainEntities
						.SelectMany(x => x.Entity.DomainEvents)
						//.Select(x => (IEvent)x) //there is a bug in FastEndpoints regarding interface inheritance, therefore we need IEvent here
						.ToList();

				domainEntities.ToList()
						.ForEach(a => a.Entity.ClearDomainEvents());

				foreach (var domainEvent in domainEvents)
						await eventPublisher.PublishAsync(domainEvent, ct);

				return domainEvents.Count;
		}
}
