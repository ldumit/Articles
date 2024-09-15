using Articles.Abstractions;
using Production.Domain.Enums;
using Stateless;

namespace Production.Application
{
		public class AssetActivity
		{
				AssetStatus _status = AssetStatus.Requested;
				StateMachine<AssetStatus, AssetActionType> _machine = null;

        public AssetActivity()
				{
            _machine = new StateMachine<AssetStatus, AssetActionType>(() => _status, s => _status = s);

						_machine.Configure(AssetStatus.Uploaded)
								.Permit(AssetActionType.Approve, AssetStatus.Approved)
								.Permit(AssetActionType.Request, AssetStatus.Requested);


				}

		}

		public class StageTransition
		{
				ArticleStage _stage = ArticleStage.Accepted;
				StateMachine<ArticleStage, ArticleActionType> _machine = null;

				public StageTransition()
				{
						_machine = new StateMachine<ArticleStage, ArticleActionType>(() => _stage, s => _stage = s);

						_machine.Configure(ArticleStage.Accepted)
								.Permit(ArticleActionType.AssignTypesetter, ArticleStage.InProduction);						
				}

		}
}
