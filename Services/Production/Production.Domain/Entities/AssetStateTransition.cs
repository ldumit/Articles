using Articles.Abstractions;
using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;


public class AssetStateTransition : IDomainObject
{
		public ArticleStage ArticleStage { get; init; }
		public Enums.AssetType AssetType { get; init; }
		public AssetStatus AssetStatus { get; init; }
		public AssetActionType ActionType { get; init; }
}