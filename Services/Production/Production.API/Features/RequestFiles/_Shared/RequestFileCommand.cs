using FluentValidation;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Enums;

namespace Production.API.Features.RequestFiles.Shared;

public abstract record RequestFileCommand : AssetCommand<RequestFilesCommandResponse>
{
    public override AssetActionType ActionType => AssetActionType.Request;

}
public abstract record RequestMultipleFilesCommand : RequestFileCommand
{

    //talk - about using List, Array, IEnumerable in the input/output models.
    //Preffer IEnumerable if for input since it doesn't change. And List for output since we need to add elements.
    public IEnumerable<AssetRequest> AssetRequests { get; set; }
    public class AssetRequest
    {
        public AssetType AssetType { get; set; }
        public byte AssetNumber { get; set; }
    }
}



public class RequestFilesCommandResponse
{
    public List<FileResponse> Assets { get; set; } = new();
}

public abstract class RequestFilesValidator<TRequestCommand> : ArticleCommandValidator<TRequestCommand>
        where TRequestCommand : RequestMultipleFilesCommand
{
    public RequestFilesValidator()
    {
        RuleFor(r => r.AssetRequests)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Request cannot be empty")
                .ForEach(r =>
                {
                    r
                    .Must(a => a.AssetNumber <= 10).WithMessage("Maximum 10 assets of the same type are allowed")
                    .Must(a => AllowedAssetTypes.Contains(a.AssetType)).WithMessage("AssetType not allowed");
                });
    }

    public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}