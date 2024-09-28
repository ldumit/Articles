using FluentValidation;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.ApproveDraftPdf;

public record ApproveDraftPdfCommand : AssetCommand<AssetActionResponse>
{
    public int AssetId { get; set; }
    public override AssetActionType ActionType => AssetActionType.Approve;
}

public class ApproveDraftPdfCommandValidator
    : ArticleCommandValidator<ApproveDraftPdfCommand>
{
		public ApproveDraftPdfCommandValidator()
		{
				this.ClassLevelCascadeMode = CascadeMode.Stop;

				RuleFor(r => r.AssetId).GreaterThan(0);

				RuleFor(r => r).MustAsync(async (r, _, cancellation) => await IsActionValid(r))
						.WithMessage("Action not allowed");
		}

		protected virtual async Task<bool> IsActionValid(ApproveDraftPdfCommand action)
		{
				var asset = await Resolve<AssetRepository>().GetByIdAsync(action.ArticleId, action.AssetId);
				var stateMachine = Resolve<AssetStateMachineFactory>()(asset.State);

				return asset != null
						&& stateMachine.CanFire(asset.Article.Stage, asset.TypeCode, action.ActionType);
		}
}
