using Articles.Entitities;
using Articles.System.Cache;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public class AssetStateTransition : IDomainMetadata, ICacheable
{
		public AssetState CurrentState{ get; set; }
		public AssetActionType ActionType { get; set; }
		public AssetState DestinationState{ get; set; }
}
