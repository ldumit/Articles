using Review.Domain.Shared.Enums;

namespace Review.Domain.Shared;

public interface IArticleAction : IArticleAction<ArticleActionType>;

//public sealed class ArticleAction : IArticleAction
//{
//		public int ArticleId { get; init; }
//		public ArticleActionType ActionType { get; init; }
//		public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
//		public int CreatedById { get; set; }
//		public string? Comment { get; init; }

//		public string Action => ActionType.ToString();
//}