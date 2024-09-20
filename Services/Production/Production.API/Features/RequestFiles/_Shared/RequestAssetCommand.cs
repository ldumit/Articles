using FluentValidation;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;
using static Production.API.Features.RequestFiles.Shared.RequestMultipleFilesCommand;

namespace Production.API.Features.RequestFiles.Shared;

public abstract record RequestAssetCommand : AssetCommand<RequestFilesCommandResponse>
{
    public override AssetActionType ActionType => AssetActionType.Request;

}
public abstract record RequestMultipleFilesCommand : RequestAssetCommand
{

    //talk - about using List, Array, IEnumerable in the input/output models.
    //Preffer IEnumerable if for input since it doesn't change. And List for output since we need to add elements.
    public IEnumerable<AssetRequest> AssetRequests { get; set; }
    public record AssetRequest(AssetType AssetType, byte AssetNumber);
}



public class RequestFilesCommandResponse
{
    public List<FileResponse> Assets { get; set; } = new();
}

public abstract class RequestFilesValidator<TRequestCommand> : ArticleCommandValidator<TRequestCommand>
        where TRequestCommand : RequestMultipleFilesCommand
{
		public RequestFilesValidator()
    {

				RuleFor(r => r.AssetRequests)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Request cannot be empty")
                .ForEach(r =>
                {
										r
										.Must(a => a.AssetNumber <= 10).WithMessage("Maximum 10 assets of the same type are allowed")
										.Must(a => AllowedAssetTypes.Contains(a.AssetType)).WithMessage("Asset type not allowed");
                });

				RuleFor(r => r).MustAsync(async (r, _, cancellation) => await IsActionValid(r))
						.WithMessage("Action not allowed");
		}


		protected virtual async Task<bool> IsActionValid(RequestMultipleFilesCommand action)
		{
				var assetRequest = action.AssetRequests.FirstOrDefault(); // we only need to validate for one asset

				var assetRepository = Resolve<AssetRepository>();
				var asset = await assetRepository.GetByTypeAndNumber(action.ArticleId, assetRequest.AssetType, assetRequest.AssetNumber);

				var stateMachineFactory = Resolve<AssetStateMachineFactory>();
				var stateMachine = stateMachineFactory(asset.State);

				
				return asset != null
						&& stateMachine.CanFire(asset.Article.Stage, asset.TypeCode, action.ActionType);

		}
		public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}


public record AssetAction(AssetActionType ActionType, int ArticleId, AssetRequest AssetRequest);

public class AssetActionValidator : AbstractValidator<AssetAction>
{
		AssetStateMachine _assetStateMachine;
		AssetRepository _assetRepository;
		
		public AssetActionValidator(AssetStateMachine assetStateMachine, AssetRepository assetRepository)
		{
				_assetStateMachine = assetStateMachine;
				_assetRepository = assetRepository;

				RuleFor(r => r).MustAsync(async (r, _ , cancellation) => await IsActionValid(r))
						.WithMessage("Action not allowed");
		}

		public virtual bool IsActionValid(int articleId, int assetId, AssetActionType actionType)
		{
        return true;
		}
		public virtual async Task<bool> IsActionValid(AssetAction action)
		{
				var asset = await _assetRepository.GetByTypeAndNumber(action.ArticleId, action.AssetRequest.AssetType, action.AssetRequest.AssetNumber);
				return asset != null 
						&& _assetStateMachine.CanFire(asset.Article.Stage, asset.TypeCode, action.ActionType);
		}
}