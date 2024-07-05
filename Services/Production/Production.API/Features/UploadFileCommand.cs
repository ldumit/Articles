using Production.API.Features.UploadAuthorsProof;
using Production.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Production.API.Features;

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
    internal override string ActionComment => this.Comment;
    internal override DiscussionType DiscussionGroupType => this.DiscussionType;
}

public abstract record UploadFileCommand : UploadFileCommand<UploadFileResponse>
{
    internal override FileActionType ActionType => FileActionType.UPLOAD;
    internal virtual byte GetAssetNumber() => 0;
}
