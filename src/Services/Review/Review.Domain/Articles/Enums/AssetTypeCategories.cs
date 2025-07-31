namespace Review.Domain.Articles.Enums;

public static class AssetTypeCategories
{
		public static HashSet<AssetType> ManuscriptAsset = new()
		{
				AssetType.Manuscript
		};

		public static HashSet<AssetType> ReviewReport = new()
		{
				AssetType.ReviewReport
		};
}