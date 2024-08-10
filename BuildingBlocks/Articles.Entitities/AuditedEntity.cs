namespace Articles.Entitities;

public interface IAuditedEntity<TUserKey>
		where TUserKey : struct
{
		public TUserKey CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public TUserKey? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }
}

public interface IAuditedEntity : IAuditedEntity<int>
{
}

[Serializable]
public abstract class AuditedEntity : AuditedEntity<int>, IEntity, IAuditedEntity
{

}

[Serializable]
public abstract class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAuditedEntity<TPrimaryKey>
		where TPrimaryKey : struct
{
		public TPrimaryKey CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public TPrimaryKey? LastModifiedById { get; set; }
		public DateTime? LasModifiedOn { get; set; }
}