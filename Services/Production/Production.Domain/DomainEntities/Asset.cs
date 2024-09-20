using Articles.Abstractions;
using Articles.Exceptions.Domain;
using Production.Domain.Enums;
using Production.Domain.Events;

namespace Production.Domain.Entities
{
		public partial class Asset
		{
				public string AssetName => Name.Replace("'", "").Replace(" ", "-");

				//todo consider asset number also
				//public string CreateFileServerId(int articleId, string fileExtension) => $"{base.CreateFileServerId(articleId)}/{AssetNumber}";
				public string CreateFileServerId(int articleId, string fileExtension) => $"{articleId}/{Name.ToLower().Replace("'", "").Replace(" ", "-")}";

				public string CreateFileName(string fileExtension) => $"{AssetName}.{fileExtension}";

				public bool IsNewVersion => this.CurrentFileLink != null;

				public bool IsFileRequested => this.CurrentFileLink?.File.StatusId == Enums.FileStatus.Requested;

				public static Asset CreateFromRequest(IArticleAction<AssetActionType> articleAction, AssetType assetType, byte assetNumber = 0)
				{
						//talk - the ideal implementation sould have been to actually encapsulate the AssetNumber in  its own class.
						// the problem is we need to validate against the assetType.MaxNumber, therefore we need to send the AssetType as a parameter.
						//todo - extract it into a value object
						ArgumentOutOfRangeException.ThrowIfGreaterThan(assetNumber, assetType.MaxNumber);

						var asset = new Asset()
						{
								Name = assetType.Name,
								TypeCode = assetType.Code,
								AssetNumber = assetNumber,
								CategoryId = assetType.DefaultCategoryId,
								State = AssetState.Requested
						};
						asset._actions.Add(
								new AssetAction() { CreatedById = articleAction.UserId, Comment = articleAction.Comment, CreatedOn = DateTime.UtcNow, TypeId = articleAction.ActionType }
								);
						//var file = new File() { Name = $"{asset.Name}.{assetType.DefaultFileExtension}", StatusId = FileStatus.Requested };
						//asset.Files.Add(file);
						//if(asset.LatestFileRef is null) 
						//    asset.LatestFileRef = new AssetLatestFile() { Asset= asset, File = file };
						//else
						//    asset.LatestFileRef.File = file;

						//file.FileActions.Add(
						//    new FileAction() { CreatedById = articleAction.UserId, Comment = articleAction.Comment, CreatedOn = DateTime.UtcNow, TypeId = articleAction.ActionType }
						//    );
						return asset;
				}
				public static Asset CreateFromUpload(AssetType assetType, string originalFileName, byte assetNumber = 0)
				{
						var asset = new Asset()
						{
								Name = assetType.Name,
								TypeCode = assetType.Code,
								AssetNumber = assetNumber,
								CategoryId = assetType.DefaultCategoryId,
								State = AssetState.Uploaded
						};

						return asset;
				}

				public void SetStatus(AssetState newStatus, IArticleAction<AssetActionType> action)
				{
						this.State = newStatus;
						this.LasModifiedOn = DateTime.UtcNow;
						this.LastModifiedById = action.UserId;
						_actions.Add(
								new AssetAction() { CreatedById = action.UserId, Comment = action.Comment, CreatedOn = DateTime.UtcNow, TypeId = action.ActionType }
						);
						//this.AddFileAction(action);
				}

				public void CancelRequest(IArticleAction<AssetActionType> action)
				{
						if (this.State != AssetState.Requested)
								throw new DomainException("Wrong status");

						//this.Status = newStatus;
						this.LasModifiedOn = DateTime.UtcNow;
						this.LastModifiedById = action.UserId;
						_actions.Add(
								new AssetAction() { CreatedById = action.UserId, Comment = action.Comment, CreatedOn = DateTime.UtcNow, TypeId = action.ActionType }
						);
						//this.AddFileAction(action);
				}

				private void AddFileAction(IArticleAction<AssetActionType> action)
				{
						var fileAction = new FileAction()
						{
								FileId = this.CurrentFile!.Id,
								TypeId = action.ActionType,
								Comment = action.Comment,
								CreatedById = action.UserId,
								CreatedOn = DateTime.UtcNow
						};
						this.CurrentFile.FileActions.Add(fileAction);

						this.CurrentFile.AddDomainEvent(new AssetActionExecutedDomainEvent(
								action,
								this.TypeCode,
								this.AssetNumber,
								this.CurrentFile));
				}
		}
}
