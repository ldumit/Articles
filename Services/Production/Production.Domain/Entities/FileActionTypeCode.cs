using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class FileActionTypeCode: EnumEntityCode
{

    public virtual FileActionType? FileActionType { get; set; }
}
