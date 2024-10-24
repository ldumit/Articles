using Submission.API.Features.RequestFiles.Shared;
using Submission.Domain.Enums;

namespace Submission.API.Features.RequestFiles.AuthorFiles;

public record RequestSupplementaryAssetsCommand : RequestMultipleAssetsCommand;

public class RequestSupplementaryAssetsValidator() 
    : RequestAssetsValidator<RequestSupplementaryAssetsCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.SupplementaryAssets;
}
