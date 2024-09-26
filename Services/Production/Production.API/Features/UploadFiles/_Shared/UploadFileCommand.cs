using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Production.API.Features.Shared;
using FluentValidation;

namespace Production.API.Features.UploadFiles.Shared;

public abstract record UploadFileCommand<TResponse> : AssetActionCommand<TResponse>
    where TResponse : IAssetActionResponse
{
    /// <summary>
    /// The asset type of the file.
    /// </summary>
    [Required]
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public AssetType AssetType { get; set; }

    /// <summary>
    /// The file to be uploaded.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
}

public abstract record UploadFileCommand : UploadFileCommand<FileResponse>
{
    public override AssetActionType ActionType => AssetActionType.Upload;
    //todo remove the following method 
    internal virtual byte GetAssetNumber() => 0;
}

public record FileResponse(int AssetId, AssetState State, int? FileId, int? Version, string? FileServerId) : IAssetActionResponse;
//public record AssetResponse(int Id, AssetState State, FileDto? File) : IFileActionResponse;


public abstract class UploadFileValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
				where TUploadFileCommand : UploadFileCommand
{
		public UploadFileValidator()
		{
				RuleFor(r => r.AssetType).Must(a => AllowedAssetTypes.Contains(a)).WithMessage("AssetType not allowed");
		}

		public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}
