using Articles.Abstractions.Events.Dtos;
using FileStorage.Contracts;
using Review.Domain.Articles.Enums;
using Review.Domain.Articles.Events;
using Review.Domain.Articles.ValueObjects;

namespace Review.Domain.Articles;

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
						State = AssetState.None,
						//CreatedById = action.CreatedById,
						//CreatedOn = action.CreatedOn
				};
		}


		public File CreateFile(FileMetadata fileMetadata, AssetTypeDefinition assetType, IArticleAction action)
		{
				File = File.CreateFile(fileMetadata, this, assetType);

				State = AssetState.Uploaded;

				this.AddDomainEvent(new FileUploaded(this, action));
				return File;
		}

		public static Asset CreateFromSubmission(AssetDto assetDto, AssetTypeDefinition type, int articleId)
		{
				//talk - value objects for AssetName & AssetNumber, encapsulate validation						
				var asset = new Asset()
				{
						ArticleId = articleId,
						//Article = article,
						Name = AssetName.FromAssetType(type),
						Type = type.Id,
						//CategoryId = type.DefaultCategoryId,
						State = AssetState.Uploaded,
				};

				asset.File = File.CreateFile(assetDto.File, type);

				return asset;
		}
}
