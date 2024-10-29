using Submission.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Submission.Application.Features.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Submission.Application.Features.UploadFiles.Shared;

public abstract record UploadFileCommand : ArticleCommand
{
    /// <summary>
    /// The asset type of the file.
    /// </summary>
    [Required]
    public AssetType AssetType { get; init; }

    /// <summary>
    /// The file to be uploaded.
    /// </summary>
    [Required]
    public IFormFile File { get; init; } = null!;

		public override ArticleActionType ActionType => ArticleActionType.Upload;
}

//todo validate file properties:size exttensio etc
public abstract class UploadFileValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
				where TUploadFileCommand : UploadFileCommand
{
		public UploadFileValidator()
		{
				RuleFor(r => r.AssetType).Must(a => AllowedAssetTypes.Contains(a)).WithMessage("AssetType not allowed");
				//RuleFor(r => r.File.Length)
		}

		public abstract IReadOnlyCollection<AssetType> AllowedAssetTypes { get; }
}
