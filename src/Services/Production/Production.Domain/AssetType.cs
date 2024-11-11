using Articles.Abstractions.Enums;

namespace Production.Domain.Enums;

public static class AssetTypeCategories
{
		public static HashSet<AssetType> DraftAssets = new()
		{
				AssetType.DraftPdf
		};
		public static HashSet<AssetType> FinalAssets = new()
		{
				AssetType.FinalPdf,
				AssetType.FinalHtml,
				AssetType.FinalEpub
		};
		public static HashSet<AssetType> SupplementaryAssets = new()
		{
				AssetType.Manuscript,
				AssetType.Figure,
				AssetType.SupplementaryFile,
				AssetType.DataSheet
		};
}