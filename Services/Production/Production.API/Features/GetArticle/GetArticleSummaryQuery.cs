using Production.Application.Dtos;

namespace Production.API.Features.GetArticle;

public record GetArticleSummaryQuery(int ArticleId);
public record GetArticleSummaryResonse(ArticleSummaryDto ArticleSummary);

