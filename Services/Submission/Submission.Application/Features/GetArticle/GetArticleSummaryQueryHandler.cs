using Mapster;
using MediatR;
using Submission.Application.Dtos;
using Submission.Persistence.Repositories;

namespace Submission.Application.Features.GetArticle;

public class GetArticleSummaryQueryHandler(ArticleRepository _articleRepository)
		: IRequestHandler<GetArticleQuery, GetArticleResonse>
{
		public async Task<GetArticleResonse> Handle(GetArticleQuery command, CancellationToken ct)
		{
				var article = await _articleRepository.GetFullArticleById(command.ArticleId);

				return new GetArticleResonse(article.Adapt<ArticleDto>());
		}
}