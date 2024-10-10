using Articles.Domain;
using System.Collections.Immutable;

namespace Articles.Entitities;

public interface IAggregateEntity : IAggregateEntity<int>
{
}

public interface IAggregateEntity<TPrimaryKey> : IEntity<TPrimaryKey>
		where TPrimaryKey : struct
{
		public TPrimaryKey CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public TPrimaryKey? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }
}

public abstract class AggregateEntity : AggregateEntity<int>, IEntity, IAggregateEntity
{

}

public abstract class AggregateEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateEntity<TPrimaryKey>
		where TPrimaryKey : struct
{
		//talk - audited properties are required only in the aggregates because when we are saving the other entities they are going to be part of an aggregate
		// therefore they are going to inherit the same audited values
		public TPrimaryKey CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public TPrimaryKey? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }

		#region Domain Events
		// talk if you want your colletions to be trully imutable we need to use ImmutableList, the others can be cast back to its original class
		private ImmutableList<IDomainEvent> _domainEvents = ImmutableList<IDomainEvent>.Empty;
		public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;		
		public void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);		
		public void ClearDomainEvents() => _domainEvents.Clear();
		#endregion
}