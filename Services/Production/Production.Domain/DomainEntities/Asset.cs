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

				public bool IsNewVersion => this.LatestFileRef != null;

				public bool IsFileRequested => this.LatestFileRef?.File.StatusId == Enums.FileStatus.Requested;

				public void SetStatus(AssetStatus newStatus, IArticleAction action)
				{
						this.Status = newStatus;
						this.LasModifiedOn = DateTime.UtcNow;
						this.LastModifiedById = action.UserId;

						this.AddFileAction(action);
				}

				private void AddFileAction(IArticleAction action)
				{
						var fileAction = new FileAction()
						{
								FileId = this.LatestFileId,
								TypeId = action.ActionType,
								Comment = action.Comment,
								CreatedById = action.UserId,
								CreatedOn = DateTime.UtcNow
						};
						this.LatestFile.FileActions.Add(fileAction);

						this.LatestFile.AddDomainEvent(new ActionExecutedDomainEvent(
								action,
								this.TypeCode,
								this.AssetNumber,
								this.LatestFile));
				}
		}
}
