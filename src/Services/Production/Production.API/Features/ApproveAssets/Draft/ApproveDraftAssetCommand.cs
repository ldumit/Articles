using FluentValidation;
using Production.API.Features.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.ApproveAssets.ApproveDraftAsset;

public record ApproveDraftAssetCommand : AssetCommand<AssetActionResponse>
{
    public int AssetId { get; set; }
    public override AssetActionType ActionType => AssetActionType.Approve;
}

public class ApproveDraftAssetCommandValidator
    : ArticleCommandValidator<ApproveDraftAssetCommand>
{
    public ApproveDraftAssetCommandValidator()
    {
        //ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(r => r.AssetId).GreaterThan(0);
    }
}
