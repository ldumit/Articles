using Production.Domain.Enums;
using Stateless;

namespace Production.Application
{
		public class AssetActivity
		{
				AssetStatus _status = AssetStatus.Requested;
				StateMachine<AssetStatus, ActionType> _machine = null;

        public AssetActivity()
				{
            _machine = new StateMachine<AssetStatus, ActionType>(() => _status, s => _status = s);

						_machine.Configure(AssetStatus.Uploaded)
								.Permit(ActionType.Approve, AssetStatus.Approved)
								.Permit(ActionType.RequestNew, AssetStatus.Requested);


				}

		}

		public class StageTransition
		{
				ArticleStage _stage = ArticleStage.Accepted;
				StateMachine<ArticleStage, ActionType> _machine = null;

				public StageTransition()
				{
						_machine = new StateMachine<ArticleStage, ActionType>(() => _stage, s => _stage = s);

						_machine.Configure(ArticleStage.Accepted)
								.Permit(ActionType.AssignTypesetter, ArticleStage.InProduction);						
				}

		}
}
