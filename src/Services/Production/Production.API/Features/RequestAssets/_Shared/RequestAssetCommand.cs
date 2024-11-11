using Articles.Abstractions.Enums;
using FluentValidation;
using Production.API.Features.Shared;
using Production.Application.Dtos;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.Shared;

public abstract record RequestAssetCommand : AssetCommand<RequestAssetsResponse>
{
    public override AssetActionType ActionType => AssetActionType.Request;

}
public abstract record RequestMultipleAssetsCommand : RequestAssetCommand
{
    //talk - about using List, Array, IEnumerable in the input/output models.
    //Preffer IEnumerable if for input since it doesn't change. And List for output since we need to add elements.
    public IEnumerable<AssetRequest> AssetRequests { get; set; }
    public record AssetRequest(AssetType AssetType, byte AssetNumber);
}

public class RequestAssetsResponse
{
    public IEnumerable<AssetMinimalDto> Assets { get; set; }
}

public abstract class RequestAssetsValidator<TRequestCommand> : ArticleCommandValidator<TRequestCommand>
        where TRequestCommand : RequestMultipleAssetsCommand
{
		public RequestAssetsValidator()
    {
				this.ClassLevelCascadeMode = CascadeMode.Stop;
				RuleFor(r => r.AssetRequests)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Request cannot be empty")
                .ForEach(r =>
                {
										r
										.Must(a => a.AssetNumber <= 10).WithMessage("Maximum 10 assets of the same type are allowed")
										.Must(a => AllowedAssetTypes.Contains(a.AssetType)).WithMessage("Asset type not allowed");
                });

				//RuleFor(r => r).MustAsync(async (r, _, cancellation) => await IsActionValid(r))
				//		.WithMessage("Action not allowed");
		}


		protected virtual async Task<bool> IsActionValid(RequestMultipleAssetsCommand action)
		{
				var assetRequest = action.AssetRequests.FirstOrDefault(); // we only need to validate for one asset

				var articleRepository = Resolve<ArticleRepository>();
				var article = await articleRepository.GetByIdWithSingleAssetAsync(action.ArticleId, assetRequest.AssetType, assetRequest.AssetNumber);
				if (article == null) 
						return false;

				var asset = article.Assets.SingleOrDefault();
				var assetState = asset?.State ?? AssetState.None;

				var stateMachineFactory = Resolve<AssetStateMachineFactory>();
				var stateMachine = stateMachineFactory(assetState);
				
				return stateMachine.CanFire(article.Stage, assetRequest.AssetType, action.ActionType);
		}
		public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}


//public record AssetAction(AssetActionType ActionType, int ArticleId, AssetRequest AssetRequest);

//public class AssetActionValidator : AbstractValidator<AssetAction>
//{
//		AssetStateMachine _assetStateMachine;
//		AssetRepository _assetRepository;
		
//		public AssetActionValidator(AssetStateMachine assetStateMachine, AssetRepository assetRepository)
//		{
//				_assetStateMachine = assetStateMachine;
//				_assetRepository = assetRepository;

//				RuleFor(r => r).MustAsync(async (r, _ , cancellation) => await IsActionValid(r))
//						.WithMessage("Action not allowed");
//		}

//		public virtual bool IsActionValid(int articleId, int assetId, AssetActionType actionType)
//		{
//        return true;
//		}

//		public virtual async Task<bool> IsActionValid(AssetAction action)
//		{
//				var asset = await _assetRepository.GetByTypeAndNumberAsync(action.ArticleId, action.AssetRequest.AssetType, action.AssetRequest.AssetNumber);
//				return asset != null 
//						&& _assetStateMachine.CanFire(asset.Article.Stage, asset.Type, action.ActionType);
//		}
//}