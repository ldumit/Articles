using Articles.Entitities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;

namespace Articles.EntityFrameworkCore;

public static class MediatorExtension
{
		//todo unify the following 2 methods
    public static async Task<int> DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<IAggregateEntity>();
            //.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);

        return domainEvents.Count;
    }

		public static async Task<int> DispatchDomainEventsAsync(this DbContext ctx, CancellationToken cancellationToken = default)
		{
				var domainEntities = ctx.ChangeTracker
						.Entries<IAggregateEntity>();

				var domainEvents = domainEntities
						.SelectMany(x => x.Entity.DomainEvents)
						.Select(x => (IEvent)x) //there is a bug in FastEndpoints regarding interface inheritance, therefore we need IEvent here
						.ToList();

				domainEntities.ToList()
						.ForEach(a => a.Entity.ClearDomainEvents());

				foreach (var domainEvent in domainEvents)
						await domainEvent.PublishAsync(Mode.WaitForAll, cancellationToken);

				return domainEvents.Count;
		}
}
