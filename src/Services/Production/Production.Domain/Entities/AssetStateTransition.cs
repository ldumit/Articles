using Blocks.Entitities;
using Blocks.Core.Cache;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public class AssetStateTransition : IMetadataEntity, ICacheable
{
		public AssetState CurrentState{ get; set; }
		public AssetActionType ActionType { get; set; }
		public AssetState DestinationState{ get; set; }
}
