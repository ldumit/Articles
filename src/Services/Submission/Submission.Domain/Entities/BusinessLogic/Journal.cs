using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Mapster;
using Submission.Domain.Events;

namespace Submission.Domain.Entities;

public partial class Journal
{
		public Article CreateArticle(string title, ArticleType Type, string scope, int journalId, IArticleAction action)
		{
				var article = new Article
				{
						Title = title,
						Type = Type,
						Scope = scope,
						JournalId = journalId,
						Stage = ArticleStage.Created,
						CreatedById = action.CreatedById,
						CreatedOn = action.CreatedOn
				};

				action.Adapt(article);

				//todo - do we need to add the article to the journal? is it setting the journalId enough?
				_articles.Add(article);

				var domainEvent = article.Adapt<ArticleCreatedDomainEvent>() with { Action = action };
				article.AddDomainEvent(domainEvent);
				return article;
		}
}
