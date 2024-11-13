using Articles.Abstractions.Enums;
using Blocks.Entitities;
using Blocks.Core.Cache;
using System.Collections.Immutable;

namespace Production.Domain.Entities;

public class AssetStateTransitionCondition : Entity, ICacheable
{
		public ArticleStage ArticleStage { get; set; }
		public IReadOnlyList<AssetType> AssetTypes { get; init; } = ImmutableList<AssetType>.Empty;
		public IReadOnlyList<Enums.AssetActionType> ActionTypes { get; init; } = ImmutableList<Enums.AssetActionType>.Empty;
}
