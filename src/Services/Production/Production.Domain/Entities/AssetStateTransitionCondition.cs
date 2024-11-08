using Articles.Abstractions;
using Articles.Entitities;
using Articles.System.Cache;
using System.Collections.Immutable;

namespace Production.Domain.Entities;

public class AssetStateTransitionCondition : Entity, ICacheable
{
		public ArticleStage ArticleStage { get; set; }
		public IReadOnlyList<Enums.AssetType> AssetTypes { get; init; } = ImmutableList<Enums.AssetType>.Empty;
		public IReadOnlyList<Enums.AssetActionType> ActionTypes { get; init; } = ImmutableList<Enums.AssetActionType>.Empty;
}
