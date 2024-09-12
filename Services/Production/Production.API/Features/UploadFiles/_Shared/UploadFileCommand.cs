using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Articles.Abstractions;
using Production.API.Features.Shared;

namespace Production.API.Features.UploadFiles.Shared;

public abstract record UploadFileCommand<TResponse> : FileActionCommand<TResponse>
    where TResponse : IFileActionResponse
{
    /// <summary>
    /// The asset type of the file.
    /// </summary>
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AssetType AssetType { get; set; }

    /// <summary>
    /// The file to be uploaded.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }

    /// <summary>
    /// Comment, if any.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// The type of Discussion, whether it is Public or Private.
    /// </summary>
    [DefaultValue(DiscussionType.Default)]
    public DiscussionType DiscussionType { get; set; }

    /// <summary>
    /// The Batch Id for uploading multiple files in a batch.
    /// </summary>
    public Guid? BatchId { get; set; }
    internal string FileName { get; set; }
    internal string FileServerId { get; set; }
    internal int VersionCount { get; set; }
    protected override string GetActionComment() => Comment;
}

public abstract record UploadFileCommand : UploadFileCommand<UploadFileResponse>
{
    protected override ActionType GetActionType() => ActionType.Upload;
    //todo remove the following method 
    internal virtual byte GetAssetNumber() => 0;
}

public record UploadFileResponse(int Assetid, int FileId, int Version, string FileServerId) : IFileActionResponse;
