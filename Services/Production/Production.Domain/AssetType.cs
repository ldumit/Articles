namespace Production.Domain.Enums;

public enum AssetType
{
		Manuscript,
		ReviewReport,
		DraftPdf,
		FinalPdf,
		FinalHtml,
		FinalEpub,
		Figure,
		DataSheet,
		SupplementaryFile
}

public static class AssetTypeCategories
{
		public static HashSet<AssetType> FinalFiles = new()
		{
				AssetType.FinalPdf,
				AssetType.FinalHtml,
				AssetType.FinalEpub
		};
		public static HashSet<AssetType> AuthorFiles = new()
		{
				AssetType.Manuscript,
				AssetType.Figure,
				AssetType.SupplementaryFile,
				AssetType.DataSheet
		};
}