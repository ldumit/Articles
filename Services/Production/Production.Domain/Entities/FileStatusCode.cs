using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class FileStatusCode: EnumEntityCode
{
    public virtual FileStatus? FileStatus { get; set; }
}
