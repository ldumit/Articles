using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;
using Production.Application.StateMachines;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.AuthorFiles;

public record RequestAuthorFilesCommand : RequestMultipleFilesCommand;

public class RequestAuthorFilesValidator() 
    : RequestFilesValidator<RequestAuthorFilesCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.AuthorFiles;
}
