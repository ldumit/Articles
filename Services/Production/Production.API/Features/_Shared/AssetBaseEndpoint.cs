using FastEndpoints;
using Production.Domain.Entities;
using Articles.Abstractions;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using AssetType = Production.Domain.Enums.AssetType;

namespace Production.API.Features.Shared;

public abstract class AssetBaseEndpoint<TCommand, TResponse>(AssetStateMachineFactory _stateMachineFactory) 
		: Endpoint<TCommand, TResponse>
		where TCommand : IArticleAction
{
		protected Article? _article;

		protected virtual ArticleStage NextStage => _article.Stage;

		//protected virtual void CheckAndThrowStateTransition(Article article, AssetState assetState, AssetType assetType, AssetActionType actionType)
		//{
		//		if(!_stateMachineFactory(assetState).CanFire(article.Stage, assetType, actionType))
		//				throw new BadHttpRequestException("Action not allowed");
		//}

		protected virtual void CheckAndThrowStateTransition(Asset asset, AssetActionType actionType)
		{
				if (!_stateMachineFactory(asset.State).CanFire(asset.Article.Stage, asset.Type, actionType))
						throw new BadHttpRequestException("Action not allowed");
		}
}
