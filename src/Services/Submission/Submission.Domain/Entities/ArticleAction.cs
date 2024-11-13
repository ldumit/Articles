using Blocks.Entitities;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

//talk - modification never happens for an action

public partial class ArticleAction : Entity
{
    public int EntityId { get; set; }

    public string Comment { get; set; } = default!;

    public ArticleActionType TypeId { get; set; }

		public int CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
}

//public class ArticleAction : Action<ArticleActionType>;
//public class AssetAction : Action<AssetActionType>;


//public partial class Action<TActionType> : AggregateEntity
//		where TActionType : struct, Enum
//{
//		public int AssetId { get; set; }

//		//talk - difference between default! & null!
//		public string Comment { get; set; } = default!;

//		public TActionType TypeId { get; set; }

//		public virtual Asset Asset { get; set; } = null!;
//}
