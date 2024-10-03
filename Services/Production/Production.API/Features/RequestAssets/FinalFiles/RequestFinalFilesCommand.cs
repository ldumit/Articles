using Production.API.Features.RequestFiles.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Production.Persistence.Repositories;

namespace Production.API.Features.RequestFiles.FinalFiles;

public record RequestFinalFilesCommand : RequestMultipleFilesCommand;

public class RequestFinalFilesValidator() 
    : RequestFilesValidator<RequestFinalFilesCommand>()
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.FinalFiles;
}