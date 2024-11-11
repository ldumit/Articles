using Production.Persistence.Repositories;
using Articles.Abstractions.Enums;

namespace Production.Application;

public class HtmlAssetProvider : AssetProviderBase
{
    public HtmlAssetProvider(AssetRepository assetRepository) : base(assetRepository) { /*empty*/ }

    public override string ContentType(string contentType) => "application/pdf";
    //public override string AssetTypeName => Description.Replace("'", "").Replace(" ", "-");
    public override string AssetName => Description.Replace("'", "").Replace(" ", "-");
    public override AssetType AssetType => AssetType.FinalHtml;
    public override AssetCategory DefaultCategory => AssetCategory.Others;

}
