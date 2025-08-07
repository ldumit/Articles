﻿using Review.Application.Features.Assets.UploadFiles.Shared;
using Review.Domain.Articles.Enums;
using Review.Domain.Assets.Enums;

namespace Review.Application.Features.Assets.UploadFiles.UploadManuscriptFile;

public record UploadManuscriptFileCommand : UploadFileCommand
{
		public override ArticleActionType ActionType => ArticleActionType.UploadReviewReport;
}

public class UploadManuscriptCommandValidator : UploadFileValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptCommandValidator(AssetTypeRepository assetTypeRepository)
            : base(assetTypeRepository) { }

    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ManuscriptAsset;
}
