using Articles.Abstractions;
using Production.Domain.Enums;
using Stateless;

namespace Production.Application.StateMachines;

public class AssetStateMachine
{
		private StateMachine<AssetStatus, AssetActionType> _stateMachine;
		private ArticleStage _currentArticleStage;
		private AssetType _assetType;

		public AssetStateMachine(ArticleStage initialArticleStage, AssetType assetType)
		{
				_currentArticleStage = initialArticleStage;
				_assetType = assetType;

				_stateMachine = new StateMachine<AssetStatus, AssetActionType>(AssetStatus.Requested);

				_stateMachine.Configure(AssetStatus.Requested)
						.PermitIf(AssetActionType.Upload, AssetStatus.Uploaded, () => CanPerformUpload())
						.PermitIf(AssetActionType.Request, AssetStatus.Requested, () => CanPerformRequestNew());

				_stateMachine.Configure(AssetStatus.Uploaded)
						.PermitIf(AssetActionType.Approve, AssetStatus.Approved, () => CanPerformApprove())
						.PermitIf(AssetActionType.Request, AssetStatus.Requested, () => CanPerformRequestNew());


				_stateMachine.Configure(AssetStatus.ScheduledForPublication)
						.OnEntry(() =>
						{
								if (_currentArticleStage == ArticleStage.Published)
								{
										_stateMachine.Fire(AssetActionType.Upload);
								}
						});
		}

		public void SetArticleStage(ArticleStage articleStage)
		{
				_currentArticleStage = articleStage;
		}

		public void SetAssetType(AssetType assetType)
		{
				_assetType = assetType;
		}

		public void Fire(AssetActionType action)
		{
				_stateMachine.Fire(action);
		}

		private bool CanPerformUpload()
		{
				// Add conditions specific to asset types if needed
				return true;
		}

		private bool CanPerformRequestNew()
		{
				if (_assetType == AssetType.FinalPdf && _currentArticleStage < ArticleStage.PublicationScheduled)
				{
						return false;
				}
				return true;
		}

		private bool CanPerformApprove()
		{
				// Add conditions specific to asset types if needed
				return true;
		}

		private bool CanPerformSchedulePublication()
		{
				// Add conditions specific to asset types if needed
				return true;
		}
}