using Articles.Security;
using FastEndpoints;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Production.Application.Dtos;
using Production.Persistence.Repositories;

namespace Production.API.Features.GetArticle;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT},{Role.TSOF}")]
[HttpGet("articles/{articleId:int}/summary")]
public class GetArticleSummaryEndpointEndpoint(ArticleRepository _articleRepository) : Endpoint<GetArticleAssetsQuery>
{
		public override async Task HandleAsync(GetArticleAssetsQuery command, CancellationToken ct)
		{
				var article = await _articleRepository.GetArticleSummaryById(command.ArticleId);

				await SendAsync(new GetArticleSummaryResonse(article.Adapt<ArticleSummaryDto>()));
		}
}

