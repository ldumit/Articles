using Articles.Abstractions.Enums;
using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.FinalFiles;

public record RequestFinalAssetsCommand : RequestMultipleAssetsCommand;

public class RequestFinalFilesValidator() 
    : RequestAssetsValidator<RequestFinalAssetsCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.FinalAssets;
}