using Articles.Abstractions;
using Articles.Exceptions.Domain;
using FileStorage.Contracts;
using Mapster;
using Submission.Domain.Enums;
using Submission.Domain.Events;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class Asset
{
		private Asset() {/* use factory method*/}

		public string GenerateStorageFilePath(string fileName) 
				=> $"Articles/{ArticleId}/{Name}/{Number}/{fileName}";
		
		public string GenerateFileName(string fileExtension)
		{
				var assetName = Name.Value.Replace("'", "").Replace(" ", "-");
				var assetNumber = Number > 0 ? Number.ToString() : string.Empty;
				return $"{assetName}{assetNumber}.{fileExtension}";
		}

		public bool IsFileRequested => this.State == AssetState.Requested;

		public static Asset Create(Article article, AssetTypeDefinition type, byte assetNumber = 0)
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

		public void SetState(AssetState newState, IArticleAction<AssetActionType> action)
		{
				this.State = newState;
				this.LasModifiedOn = DateTime.UtcNow;
				this.LastModifiedById = action.CreatedById;
				AddAction(action);
		}

		public void CancelRequest(IArticleAction<AssetActionType> action)
		{
				if (this.State != AssetState.Requested)
						throw new DomainException("Wrong status");

				//this.Status = newStatus;
				this.LasModifiedOn = DateTime.UtcNow;
				this.LastModifiedById = action.CreatedById;
				AddAction(action);
		}

		public File CreateFile(UploadResponse uploadResponse, AssetTypeDefinition assetType)
		{
				File = File.CreateFile(uploadResponse, this, assetType);
				State = AssetState.Uploaded;
				return File;
		}

		private void AddAction(IArticleAction<AssetActionType> action)
		{
				_actions.Add(action.Adapt<AssetAction>());
				AddDomainEvent(new AssetActionExecutedDomainEvent(action, this.Article.Stage, this.Type, this.Number, this.File));
		}
}
