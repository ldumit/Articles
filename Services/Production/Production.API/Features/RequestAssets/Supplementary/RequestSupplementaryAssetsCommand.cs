using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.AuthorFiles;

public record RequestSupplementaryAssetsCommand : RequestMultipleAssetsCommand;

public class RequestSupplementaryAssetsValidator() 
    : RequestAssetsValidator<RequestSupplementaryAssetsCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.SupplementaryAssets;
}
