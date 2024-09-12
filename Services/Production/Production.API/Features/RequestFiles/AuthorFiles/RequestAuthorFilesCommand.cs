using Production.API.Features.RequestFiles.Shared;
using Production.API.Features.RequestFiles.FinalFiles;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.AuthorFiles;

public record RequestAuthorFilesCommand : RequestMultipleFilesCommand;

public abstract class RequestAuthorFilesValidator : RequestFilesValidator<RequestFinalFilesCommand>
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.AuthorFiles;
}
