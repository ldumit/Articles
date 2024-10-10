namespace Articles.Entitities;

public interface IMultitenancy
{
		public int TenantId { get; set; }
}

public abstract class TenantEntity : Entity, IMultitenancy
{
		public int TenantId { get; set; }
}

public abstract class AggregateTenantEntity : AggregateEntity<int>, IMultitenancy
{
		public int TenantId { get; set; }
}
