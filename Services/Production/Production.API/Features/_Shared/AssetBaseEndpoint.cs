using FastEndpoints;
using Production.Domain.Entities;
using Articles.Abstractions;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.Shared;

public abstract class AssetBaseEndpoint<TCommand, TResponse>(AssetRepository assetRepository, AssetStateMachineFactory stateMachineFactory)
		: Endpoint<TCommand, TResponse>
		where TCommand : IArticleAction
{
		protected readonly AssetRepository _assetRepository = assetRepository;
		protected readonly AssetStateMachineFactory _stateMachineFactory = stateMachineFactory;
		protected Article _article = null!;

		protected virtual ArticleStage NextStage => _article!.Stage; // by default the stage doesn't change

		protected virtual void CheckAndThrowStateTransition(Asset asset, AssetActionType actionType)
		{
				if (!_stateMachineFactory(asset.State).CanFire(asset.Article.Stage, asset.Type, actionType))
						throw new BadHttpRequestException("Action not allowed");
		}

		protected Asset CreateAsset(Domain.Enums.AssetType assetType, byte assetNumber)
		{
				var assetTypeEntity = _assetRepository.GetAssetType(assetType);
				return _article.CreateAsset(assetTypeEntity, assetNumber);
		}
}
