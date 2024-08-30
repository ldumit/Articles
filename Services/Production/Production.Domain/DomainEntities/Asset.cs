namespace Production.Domain.Entities
{
		public partial class Asset
		{
				public string AssetName => Name.Replace("'", "").Replace(" ", "-");

				//todo consider asset number also
				//public string CreateFileServerId(int articleId, string fileExtension) => $"{base.CreateFileServerId(articleId)}/{AssetNumber}";
				public string CreateFileServerId(int articleId, string fileExtension) => $"{articleId}/{Name.ToLower().Replace("'", "").Replace(" ", "-")}";

				public string CreateFileName(string fileExtension) => $"{AssetName}.{fileExtension}";

				public bool IsNewVersion => this.LatestFile != null;

				public bool IsFileRequested => this.LatestFile?.File.StatusId == Enums.FileStatus.NEW_VERSION_REQUESTED;
		}
}
