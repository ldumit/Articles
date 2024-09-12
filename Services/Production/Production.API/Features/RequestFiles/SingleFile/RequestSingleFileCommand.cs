using Production.API.Features.RequestFiles.Shared;
using Production.API.Features.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.SingleFile;

public record RequestSingleFileCommand : RequestFileCommand
{
    public AssetType AssetType { get; set; }
    public int AssetNumber { get; init; }
}

public abstract class RequestSingleFileValidator : ArticleCommandValidator<RequestSingleFileCommand>
{
    protected RequestSingleFileValidator()
    {
        //todo validate
    }
}
