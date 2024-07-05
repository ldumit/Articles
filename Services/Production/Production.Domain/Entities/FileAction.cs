using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class FileAction : TenantEntity
{
    public int FileId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public FileActionType TypeId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual File File { get; set; } = null!;

    public virtual User? User { get; set; }
}
