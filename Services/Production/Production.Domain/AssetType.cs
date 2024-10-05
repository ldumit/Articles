namespace Production.Domain.Enums;

public enum AssetType
{
		Manuscript = 1,
		ReviewReport = 2,
		DraftPdf = 3,
		FinalPdf = 4,
		FinalHtml = 5,
		FinalEpub = 6,
		Figure = 7,
		DataSheet = 8,
		SupplementaryFile = 9
}

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