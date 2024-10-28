using FluentValidation;
using Submission.Application;
using Submission.Persistence.Repositories;
using Articles.System;
using Articles.AspNetCore;
using Submission.Domain.Enums;
using Submission.Application.Features.Shared;
using Submission.Application.Features.UploadFiles.Shared;
using Microsoft.AspNetCore.Http;

namespace Submission.Application.Features.UploadFiles.UploadDraftFile;

public record UploadDraftFileCommand : UploadFileCommand;

public abstract class UploadDraftPdfCommandValidator : UploadFileValidator<UploadDraftFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.DraftAssets;
}


public class UploadFileCommandValidator<TUploadFileCommand> : UploadActionCommandValidator<TUploadFileCommand>
   where TUploadFileCommand : UploadFileCommand
{
    public UploadFileCommandValidator(AssetProviderBase assetProvider, ArticleRepository articleRepository, AssetRepository assetRepository)
        : base(assetProvider, articleRepository, assetRepository)
    {
        RuleFor(command => command.File).Must((command, value) => IsFileSizeValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidFileSize.FormatWith(assetProvider.MaxFileSize / 1024 / 1024).ToString());
        RuleFor(command => command.AssetType).Must((command, value) => IsAssetTypeValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidAssetType.FormatWith(command.AssetType.ToDescription()));
        RuleFor(command => command.File).Must((command, value) => IsFileExtensionValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidExtension.FormatWith(assetProvider.Description, command.File.GetExtension()));
    }
    private bool IsFileSizeValid(IFormFile file)
    {
        return file.Length < AssetProvider.MaxFileSize;
    }
    public bool IsAssetTypeValid(AssetType assetType)
    {
        return assetType == AssetProvider.AssetType;
    }

    public bool IsFileExtensionValid(IFormFile file)
    {
        return true;
        //return AssetProvider.GetAvailableFileExtension().Result.Any(a => a.Extension.Extension == file.GetExtension().ToUpperCase());
    }

    protected override AssetActionType Action => AssetActionType.Upload;

}

public abstract class UploadActionCommandValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
where TUploadFileCommand : UploadFileCommand
{
    protected abstract AssetActionType Action { get; }
    protected AssetProviderBase AssetProvider { get; }
    protected ArticleRepository ArticleRepository { get; }
    protected AssetRepository AssetRepository { get; }

    public UploadActionCommandValidator(AssetProviderBase assetProvider, ArticleRepository articleRepository, AssetRepository assetRepository)
    {
        AssetProvider = assetProvider;
        ArticleRepository = articleRepository;
        AssetRepository = assetRepository;

        RuleFor(command => Action).Must((value, c) => IsActionValid(value.ArticleId, Action))
            .WithMessage(command => ValidatorsMessagesConstants.InvalidActionMessage.FormatWith("assetProvider.Description", Action.ToString()));

    }


    public virtual bool IsActionValid(int articleId, AssetActionType action)
    {
        var result = true;
        if (articleId > 0)
        {
            //var article = ArticleRepository.GetById(articleId);
            //result = AssetProvider.GetAvailableActions(article.StageId).Result.Any(a => a == action);
        }
        return result;
    }
}
