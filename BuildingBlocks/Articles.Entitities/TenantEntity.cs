namespace Articles.Entitities;

public interface IMultitenancy
{
		public int TenantId { get; set; }
}

[Serializable]
public abstract class TenantEntity : Entity, IMultitenancy
{
		public int TenantId { get; set; }
}

[Serializable]
public abstract class AuditedTenantEntity : TenantEntity, IAuditedEntity<int>
{
		public int CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public int? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }
}
