using Submission.Application.Dtos;

namespace Submission.API.Features.GetArticle;

public record GetArticleSummaryQuery(int ArticleId);
public record GetArticleSummaryResonse(ArticleSummaryDto ArticleSummary);

