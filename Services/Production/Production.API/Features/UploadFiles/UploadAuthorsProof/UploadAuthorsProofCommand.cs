using FluentValidation;
using Production.Application;
using Production.Persistence.Repositories;
using Articles.System;
using Articles.AspNetCore;
using Production.Domain.Enums;
using Production.API.Features.Shared;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.UploadFiles.UploadAuthorsProof
{
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

        protected override ActionType Action => ActionType.Upload;

    }

    public abstract class UploadActionCommandValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
    where TUploadFileCommand : UploadFileCommand
    {
        protected abstract ActionType Action { get; }
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


        public virtual bool IsActionValid(int articleId, ActionType action)
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
}
