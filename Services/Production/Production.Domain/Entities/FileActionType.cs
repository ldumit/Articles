using Articles.Entitities;
using FileActionTypeCodes = Production.Domain.Enums.FileActionTypeCode;


namespace Production.Domain.Entities;

public partial class FileActionType:EnumEntity<FileActionTypeCodes>
{

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public virtual FileActionTypeCode CodeNavigation { get; set; } = null!;

    public virtual ICollection<FileAction> FileActions { get; } = new List<FileAction>();
}
