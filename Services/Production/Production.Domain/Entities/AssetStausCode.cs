using Articles.Entitities;

namespace Production.Domain.Entities;

public partial class AssetStatusCode : EnumEntityCode
{
    public virtual AssetStatus? AssetStatus { get; set; }
}

