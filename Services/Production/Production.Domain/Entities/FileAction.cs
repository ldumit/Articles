using Articles.Entitities;
using FileActionTypeCodes = Production.Domain.Enums.FileActionTypeCode;

namespace Production.Domain.Entities;

public partial class FileAction : TenantEntity
{
    public int FileId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public FileActionTypeCodes TypeId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual File File { get; set; } = null!;

    public virtual FileActionType Type { get; set; } = null!;

    public virtual User? User { get; set; }
}
