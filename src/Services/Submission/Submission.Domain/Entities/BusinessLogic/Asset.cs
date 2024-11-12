using Articles.Abstractions;
using FileStorage.Contracts;
using Submission.Domain.Enums;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Asset
{
		private Asset() {/* use factory method*/}

		public string GenerateStorageFilePath(string fileName) 
				=> $"Articles/{ArticleId}/{Name}/{Number}/{fileName}";

		//talk - use internal factory method so that the Asset can be created only in the Domain
		internal static Asset Create(Article article, AssetTypeDefinition type, byte assetNumber = 0)
		{
				//talk - value objects for AssetName & AssetNumber, encapsulate validation						
				return new Asset()
				{
						ArticleId = article.Id,
						Article = article,
						Number = AssetNumber.FromNumber(assetNumber, type),
						Name = AssetName.FromAssetType(type),
						Type = type.Name,
						TypeRef = type,
						CategoryId = type.DefaultCategoryId,
						State = AssetState.None
				};
		}

		public File CreateFile(UploadResponse uploadResponse, AssetTypeDefinition assetType, IArticleAction<ArticleActionType> action)
		{
				File = File.CreateFile(uploadResponse, this, assetType);
				State = AssetState.Uploaded;
				return File;
		}
}
