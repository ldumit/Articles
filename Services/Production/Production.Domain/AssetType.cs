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
		public static HashSet<AssetType> DraftFiles = new()
		{
				AssetType.DraftPdf
		};
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