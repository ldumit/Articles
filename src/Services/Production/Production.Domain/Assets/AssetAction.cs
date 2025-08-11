using Production.Domain.Assets.Enums;

namespace Production.Domain.Assets;

public partial class AssetAction : AggregateRoot //talk - modification never happens for an action
{
    public int AssetId { get; set; }

    //talk - difference between default! & null!
    public string Comment { get; set; } = default!;

    public AssetActionType TypeId { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
