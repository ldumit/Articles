using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Persistence;
using Articles.Abstractions;

namespace Production.API.Features.AssignTypesetter
{
		[Authorize(Roles = "POF")]
    [HttpPut("articles/{articleId:int}/typesetter/{typesetterId:int}")]
		[Tags("Articles")]
		public class AssignTypesetterEndpoint(ProductionDbContext _dbContext, IServiceProvider serviceProvider) 
        : BaseEndpoint<AssignTypesetterCommand, ArticleCommandResponse>(serviceProvider)
    {
				public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            var article = _articleRepository.GetById(command.ArticleId);

            //var typesetter = _dbContext.Typesetters.Single(t => t.UserId == command.Body.UserId);
						var typesetter = _dbContext.Typesetters.Single(t => t.UserId == command.TypesetterId);

						//var action = (Domain.IArticleAction)command;

						article.SetTypesetter(typesetter, command);

						article.SetStage(GetNextStage(article), command);

						await _articleRepository.SaveChangesAsync();

            await SendAsync( new ArticleCommandResponse(command.ArticleId));
        }
        protected override ArticleStage GetNextStage(Article article) => ArticleStage.InProduction;
    }

    public class AssignTypesetterCommandValidator : ArticleCommandValidator<UploadAuthorsProofCommand>
    {
        public AssignTypesetterCommandValidator(ArticleRepository articleRepository, AssetRepository assetRepository)
        {
        }
    }
}
