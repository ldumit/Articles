namespace Production.Domain.Enums;

public enum AssetType
{
		Manuscript,
		ReviewReport,
		Pdf,
		Html,
		Epub,
		Figure,
		DataSheet,
		SupplementaryFile
}

public static class AssetTypeCategories
{
		public static HashSet<AssetType> FinalFiles = new()
		{
				AssetType.Pdf,
				AssetType.Html,
				AssetType.Epub
		};
		public static HashSet<AssetType> AuthorFiles = new()
		{
				AssetType.Manuscript,
				AssetType.Figure,
				AssetType.SupplementaryFile,
				AssetType.DataSheet
		};
}