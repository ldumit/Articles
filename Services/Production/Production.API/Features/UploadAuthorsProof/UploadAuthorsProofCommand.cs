using FluentValidation;
using Production.Application;
using Production.Database.Repositories;
using Articles.System;
using Articles.AspNetCore;
using Production.Domain.Enums;

namespace Production.API.Features.UploadAuthorsProof
{
    public class UploadFileResponse : IFileActionResponse
    {
        /// <summary>
        /// Returns the FileId of the uploaded file.
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// Returns the assetId of the uploaded file
        /// </summary>
        public int AssetId { get; set; }
        /// <summary>
        /// Returns the version of the uploaded file.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Returns the fileServerId of the uploaded file
        /// </summary>
        public string FileServerId { get; set; }
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
        public bool IsAssetTypeValid(ArticleAssetType assetType)
        {
            return assetType == AssetProvider.AssetType;
        }

        public bool IsFileExtensionValid(IFormFile file)
        {
            return true;
            //return AssetProvider.GetAvailableFileExtension().Result.Any(a => a.Extension.Extension == file.GetExtension().ToUpperCase());
        }

        protected override FileActionTypeCode Action => FileActionTypeCode.UPLOAD;

    }

    public abstract class UploadActionCommandValidator<TUploadFileCommand> : ArticleCommandValidator<TUploadFileCommand>
    where TUploadFileCommand : UploadFileCommand
    {
        protected abstract FileActionTypeCode Action { get; }
        protected AssetProviderBase AssetProvider { get; }
        protected ArticleRepository ArticleRepository { get; }
        protected AssetRepository AssetRepository { get; }

        public UploadActionCommandValidator(AssetProviderBase assetProvider, ArticleRepository articleRepository, AssetRepository assetRepository)
        {
            AssetProvider = assetProvider;
            ArticleRepository = articleRepository;
            AssetRepository = assetRepository;

            RuleFor(command => this.Action).Must((value, c) => IsActionValid(value.ArticleId, this.Action))
                .WithMessage(command => ValidatorsMessagesConstants.InvalidActionMessage.FormatWith("assetProvider.Description", this.Action.ToString()));

        }


        public virtual bool IsActionValid(int articleId, FileActionTypeCode action)
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
