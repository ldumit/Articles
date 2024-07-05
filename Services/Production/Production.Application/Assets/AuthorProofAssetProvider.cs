using Production.Database.Repositories;
using Production.Domain.Enums;

namespace Production.Application;

public class AuthorProofAssetProvider : AssetProviderBase
{
    public AuthorProofAssetProvider(AssetRepository assetRepository) : base(assetRepository) { /*empty*/ }

    public override string ContentType(string contentType) => "application/pdf";
    //public override string AssetTypeName => Description.Replace("'", "").Replace(" ", "-");
    public override string AssetName => Description.Replace("'", "").Replace(" ", "-");
    public override AssetType AssetType => AssetType.AUTHORS_PROOF;
    public override AssetCategory DefaultCategory => AssetCategory.OTHERS;

}
