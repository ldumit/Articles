using FluentValidation;
using Submission.Application;
using Submission.Persistence.Repositories;
using Articles.System;
using Articles.AspNetCore;
using Submission.Domain.Enums;
using Submission.Application.Features.Shared;
using Submission.Application.Features.UploadFiles.Shared;
using Microsoft.AspNetCore.Http;

namespace Submission.Application.Features.UploadFiles;

public record UploadManuscriptFileCommand : UploadFileCommand;

public abstract class UploadDraftPdfCommandValidator : UploadFileValidator<UploadManuscriptFileCommand>
{
		public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ManuscriptAsset;
}


public class UploadFileCommandValidator<TUploadFileCommand> : UploadActionCommandValidator<TUploadFileCommand>
   where TUploadFileCommand : UploadFileCommand
{
    public UploadFileCommandValidator(Domain.Entities.AssetTypeDefinition assetType, ArticleRepository articleRepository, AssetRepository assetRepository)
        : base(assetType, articleRepository, assetRepository)
    {
        RuleFor(command => command.File).Must((command, value) => IsFileSizeValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidFileSize.FormatWith(assetType.MaxFileSizeInMB / 1024 / 1024).ToString());
        RuleFor(command => command.AssetType).Must((command, value) => IsAssetTypeValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidAssetType.FormatWith(command.AssetType.ToDescription()));
        RuleFor(command => command.File).Must((command, value) => IsFileExtensionValid(value))
              .WithMessage(command => ValidatorsMessagesConstants.InvalidExtension.FormatWith(assetType.Description, command.File.GetExtension()));
    }
    private bool IsFileSizeValid(IFormFile file)
    {
        return file.Length < AssetType.MaxFileSizeInMB;
    }
    public bool IsAssetTypeValid(AssetType assetType)
    {
        return assetType == AssetType.Id;
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
    protected Domain.Entities.AssetTypeDefinition AssetType { get; }
    protected ArticleRepository ArticleRepository { get; }
    protected AssetRepository AssetRepository { get; }

    public UploadActionCommandValidator(Domain.Entities.AssetTypeDefinition assetType, ArticleRepository articleRepository, AssetRepository assetRepository)
    {
        AssetType = assetType;
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
