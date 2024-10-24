using Articles.Security;
using FastEndpoints;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Submission.Application.Dtos;
using Submission.Persistence.Repositories;

namespace Submission.API.Features.GetArticle;

[Authorize(Roles = $"{Role.POF},{Role.CORAUT},{Role.TSOF}")]
[HttpGet("articles/{articleId:int}/assets")]
public class GetArticleAssetsEndpointEndpoint(ArticleRepository _articleRepository) : Endpoint<GetArticleAssetsQuery>
{

		public override async Task HandleAsync(GetArticleAssetsQuery command, CancellationToken ct)
		{
				var article = await _articleRepository.GetArticleWithAssetsById(command.ArticleId);

				await SendAsync(new GetArticleAssetsResponse(article.Assets.Adapt<IReadOnlyList<AssetDto>>()));
		}
}

