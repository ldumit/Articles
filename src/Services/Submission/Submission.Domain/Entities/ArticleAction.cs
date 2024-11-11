using Articles.Entitities;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

//talk - modification never happens for an action

//todo - ArticleAction is not an aggregate root but it contains CreatedBy & CreatedOn
public partial class ArticleAction : AggregateEntity
{
    public int EntityId { get; set; }

    public string Comment { get; set; } = default!;

    public ArticleActionType TypeId { get; set; }

    //public virtual Asset Asset { get; set; } = null!;
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
