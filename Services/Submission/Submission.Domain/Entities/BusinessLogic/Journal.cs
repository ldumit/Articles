using Articles.Abstractions;
using Articles.Entitities;
using Mapster;
using Submission.Domain.Enums;
using Submission.Domain.Events;

namespace Submission.Domain.Entities;

public partial class Journal
{
		public Article CreateArticle(string title, ArticleType Type, string ScopeStatement, int journalId, IArticleAction action)
		{
				var article = new Article
				{
						Title = title,
						Type = Type,
						Scope = ScopeStatement,
						JournalId = journalId,
						Stage = ArticleStage.ArticleCreated
				};
				_articles.Add(article);

				var domainEvent = article.Adapt<ArticleCreatedDomainEvent>() with { Action = action };
				article.AddDomainEvent(domainEvent);
				return article;
		}
}
