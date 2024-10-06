using Articles.Abstractions;
using Articles.Exceptions.Domain;
using FileStorage.Contracts;
using Mapster;
using Production.Domain.Enums;
using Production.Domain.ValueObjects;

namespace Production.Domain.Entities;

public partial class Asset
{
		private Asset() {/* use factory method*/}

		public string GenerateStorageFilePath(string fileName) 
				=> $"Articles/{ArticleId}/{Name}/{Number}/{CalculateNextVersion()}/{fileName}";
		
		public byte CalculateNextVersion() => (byte)(CurrentVersion + 1);
		public byte CurrentVersion => CurrentFile?.Version.Value ?? 0;

		public string GenerateFileName(string fileExtension)
		{
				var assetName = Name.Value.Replace("'", "").Replace(" ", "-");
				var assetNumber = Number > 0 ? Number.ToString() : string.Empty;
				return $"{assetName}{assetNumber}.{fileExtension}";
		}

		public bool IsNewVersion => this.CurrentFileLink != null;

		public bool IsFileRequested => this.State == AssetState.Requested;

		public static Asset CreateFromRequest(IArticleAction<AssetActionType> articleAction, AssetType assetType, byte assetNumber = 0)
		{
				return Create(articleAction, assetType, AssetState.Requested, assetNumber);
		}
		public static Asset CreateFromUpload(IArticleAction<AssetActionType> articleAction, AssetType assetType, byte assetNumber = 0)
		{
				return Create(articleAction, assetType, AssetState.Uploaded, assetNumber);
		}

		private static Asset Create(IArticleAction<AssetActionType> articleAction, AssetType type, AssetState state, byte assetNumber = 0)
		{
				//talk - value objects for AssetName & AssetNumber, encapsulate validation						
				var asset = new Asset()
				{
						ArticleId = articleAction.ArticleId,
						Number = AssetNumber.FromNumber(assetNumber, type),
						Name = AssetName.FromAssetType(type),
						Type = type.Code,
						TypeRef = type,
						CategoryId = type.DefaultCategoryId,
						State = state
				};
				asset._actions.Add(articleAction.Adapt<AssetAction>());

				return asset;
		}

		public void SetState(AssetState newStatus, IArticleAction<AssetActionType> action)
		{
				this.State = newStatus;
				this.LasModifiedOn = DateTime.UtcNow;
				this.LastModifiedById = action.CreatedById;
				_actions.Add(
						new AssetAction() { CreatedById = action.CreatedById, Comment = action.Comment, CreatedOn = DateTime.UtcNow, TypeId = action.ActionType }
				);
		}

		public void CancelRequest(IArticleAction<AssetActionType> action)
		{
				if (this.State != AssetState.Requested)
						throw new DomainException("Wrong status");

				//this.Status = newStatus;
				this.LasModifiedOn = DateTime.UtcNow;
				this.LastModifiedById = action.CreatedById;
				_actions.Add(
						new AssetAction() { CreatedById = action.CreatedById, Comment = action.Comment, CreatedOn = DateTime.UtcNow, TypeId = action.ActionType }
				);
				//this.AddFileAction(action);
		}

		public File CreateAndAddFile(UploadResponse uploadResponse)
		{
				var file = File.CreateFile(uploadResponse, this);

				_files.Add(file);
				CurrentFileLink = new AssetCurrentFileLink() { File = file };
				State = AssetState.Uploaded;
				return file;
		}
}
