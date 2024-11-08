using Production.Application.Dtos;

namespace Production.API.Features.GetArticle;

public record GetArticleAssetsQuery(int ArticleId);
public record GetArticleAssetsResponse(IReadOnlyList<AssetDto> Assets);

