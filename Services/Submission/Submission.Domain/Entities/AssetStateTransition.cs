using Articles.Entitities;
using Articles.System.Cache;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public class AssetStateTransition : IMetadataEntity, ICacheable
{
		public AssetState CurrentState{ get; set; }
		public AssetActionType ActionType { get; set; }
		public AssetState DestinationState{ get; set; }
}
