using Mapster;
using Production.Application.Dtos;
using Production.Persistence.Repositories;

namespace Production.API.Features.Articles.GetArticle;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT},{Role.TSOF}")]
[HttpGet("articles/{articleId:int}/assets")]
public class GetArticleAssetsEndpointEndpoint(ArticleRepository _articleRepository) : Endpoint<GetArticleAssetsQuery>
{
		public override async Task HandleAsync(GetArticleAssetsQuery command, CancellationToken ct)
		{
				var article = await _articleRepository.GetArticleAssetsById(command.ArticleId);

				await SendAsync(new GetArticleAssetsResponse(article.Assets.Adapt<IReadOnlyList<AssetDto>>()));
		}
}