using Review.Application.Dtos;

namespace Review.Application.Features.GetArticle;

public class GetArticleQueryHandler(ArticleRepository _articleRepository)
		: IRequestHandler<GetArticleQuery, GetArticleResonse>
{
		public async Task<GetArticleResonse> Handle(GetArticleQuery command, CancellationToken ct)
		{
				var article = await _articleRepository.GetFullArticleByIdOrThrow(command.ArticleId);

				return new GetArticleResonse(article.Adapt<ArticleDto>());
		}
}