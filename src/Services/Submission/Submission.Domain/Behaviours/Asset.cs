using FileStorage.Contracts;

namespace Submission.Domain.Entities;

public partial class Asset
{
		private Asset() {/* use factory method*/}

		public string GenerateStorageFilePath(string fileName) 
				=> $"Articles/{ArticleId}/{Name}/{fileName}";

		//talk - use internal factory method so that the Asset can be created only in the Domain
		internal static Asset Create(Article article, AssetTypeDefinition type)
		{
				//talk - value objects for AssetName & AssetNumber, encapsulate validation						
				return new Asset()
				{
						ArticleId = article.Id,
						Article = article,
						Name = AssetName.FromAssetType(type),
						Type = type.Name,
						TypeRef = type,
						CategoryId = type.DefaultCategoryId,
						State = AssetState.None
				};
		}

		public File CreateFile(FileMetadata fileMetadata, AssetTypeDefinition assetType, IArticleAction<ArticleActionType> action)
		{
				File = File.CreateFile(fileMetadata, this, assetType);
				State = AssetState.Uploaded;
				return File;
		}
}
