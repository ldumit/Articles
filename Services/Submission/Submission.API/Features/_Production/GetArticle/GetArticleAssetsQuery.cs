using Submission.Application.Dtos;

namespace Submission.API.Features.GetArticle;

public record GetArticleAssetsQuery(int ArticleId);
public record GetArticleAssetsResponse(IReadOnlyList<AssetDto> Assets);

