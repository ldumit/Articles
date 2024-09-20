using Articles.Abstractions;
using Production.Domain.Enums;
using Stateless;

namespace Production.Application
{
		public class AssetActivity
		{
				AssetState _status = AssetState.Requested;
				StateMachine<AssetState, AssetActionType> _machine = null;

        public AssetActivity()
				{
            _machine = new StateMachine<AssetState, AssetActionType>(() => _status, s => _status = s);

						_machine.Configure(AssetState.Uploaded)
								.Permit(AssetActionType.Approve, AssetState.Approved)
								.Permit(AssetActionType.Request, AssetState.Requested);


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
