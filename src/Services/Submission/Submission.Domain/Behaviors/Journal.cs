using Mapster;

namespace Submission.Domain.Entities;

public partial class Journal
{
		public Article CreateArticle(string title, ArticleType Type, string scope, IArticleAction action)
		{
				var article = new Article
				{
						Title = title,
						Type = Type,
						Scope = scope,
						//JournalId = journalId,
						Journal = this,
						Stage = ArticleStage.Created,
						CreatedById = action.CreatedById,
						CreatedOn = action.CreatedOn
				};
				action.Adapt(article);

				_articles.Add(article);

				var domainEvent = new ArticleCreated(article, action);
				article.AddDomainEvent(domainEvent);
				return article;
		}
}
