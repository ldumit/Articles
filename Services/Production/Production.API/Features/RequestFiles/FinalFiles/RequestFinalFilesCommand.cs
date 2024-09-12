using Production.API.Features.RequestFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.FinalFiles;

public record RequestFinalFilesCommand : RequestMultipleFilesCommand;

public abstract class RequestFinalFilesValidator : RequestFilesValidator<RequestFinalFilesCommand>
{
    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.FinalFiles;
}