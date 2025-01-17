using Review.Application.Features.Articles.UploadFiles._Shared;

namespace Review.Application.Features.Articles.UploadFiles.UploadManuscriptFile;

public record UploadManuscriptFileCommand : UploadFileCommand;

public class UploadManuscriptCommandValidator : UploadFileValidator<UploadManuscriptFileCommand>
{
    public UploadManuscriptCommandValidator(AssetTypeRepository assetTypeRepository)
            : base(assetTypeRepository) { }

    public override IReadOnlyCollection<AssetType> AllowedAssetTypes => AssetTypeCategories.ManuscriptAsset;
}
