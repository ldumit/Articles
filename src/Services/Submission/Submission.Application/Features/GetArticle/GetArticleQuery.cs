using Articles.MediatR;
using FluentValidation;
using Submission.Application.Dtos;

namespace Submission.Application.Features.GetArticle;

public record GetArticleQuery(int ArticleId) : IQuery<GetArticleResonse>;
public record GetArticleResonse(ArticleDto ArticleSummary);

public class GetArticleValidator : AbstractValidator<GetArticleQuery>
{
		public GetArticleValidator()
		{
				RuleFor(x => x.ArticleId)
						.GreaterThan(0).WithMessage("Invalid ArticleId.");
		}
}