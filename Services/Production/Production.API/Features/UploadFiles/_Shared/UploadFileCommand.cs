using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Articles.Abstractions;
using Production.API.Features.Shared;
using Production.API.Features.RequestFiles.Shared;
using FluentValidation;

namespace Production.API.Features.UploadFiles.Shared;

public abstract record UploadFileCommand<TResponse> : FileActionCommand<TResponse>
    where TResponse : IFileActionResponse
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

public class FileResponse2
{
    public int FileId { get; set; }
    public int Version { get; set; }
    public FileStatus2 Status { get; set; }
}
public class FileResponse3
{
		public int AssetId { get; set; }
		public int FileId { get; set; }
		public int Version { get; set; }
		public FileStatus2 Status { get; set; }
}




public record FileResponse(int AssetId, AssetState State, int? FileId, int? Version, string? FileServerId) : IFileActionResponse;
//public record AssetResponse(int Id, AssetState State, FileDto? File) : IFileActionResponse;

public record AssetResponse : IFileActionResponse
{
    public int Id { get; set; }		
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
		public AssetState State { get; set; }
    public FileDto? File { get; set; }
}

public record FileDto(int FileId, int Version, string FileServerId);

public abstract class UploadFileValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
				where TUploadFileCommand : UploadFileCommand
{
		public UploadFileValidator()
		{
				RuleFor(r => r.AssetType).Must(a => AllowedAssetTypes.Contains(a)).WithMessage("AssetType not allowed");
		}

		public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}
