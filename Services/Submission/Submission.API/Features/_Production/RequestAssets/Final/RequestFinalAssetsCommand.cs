using Submission.API.Features.RequestFiles.Shared;
using Submission.Domain.Enums;

namespace Submission.API.Features.RequestFiles.FinalFiles;

public record RequestFinalAssetsCommand : RequestMultipleAssetsCommand;

public class RequestFinalFilesValidator() 
    : RequestAssetsValidator<RequestFinalAssetsCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.FinalAssets;
}