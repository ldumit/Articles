using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.AuthorFiles;

public record RequestAuthorFilesCommand : RequestMultipleAssetsCommand;

public class RequestAuthorFilesValidator() 
    : RequestAssetsValidator<RequestAuthorFilesCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.AuthorFiles;
}
