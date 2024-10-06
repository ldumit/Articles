using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.Persistence;
using Articles.Abstractions;
using Production.API.Features.Shared;

namespace Production.API.Features.AssignTypesetter
{
		[Authorize(Roles = "POF")]
    [HttpPut("articles/{articleId:int}/typesetter/{typesetterId:int}")]
		[Tags("Articles")]
		public class AssignTypesetterEndpoint(ArticleRepository articleRepository, ProductionDbContext _dbContext) 
        : BaseEndpoint<AssignTypesetterCommand, ArticleCommandResponse>(articleRepository)
    {
				public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            var article = await _articleRepository.GetByIdAsync(command.ArticleId, throwNotFound:true);

						//todo - maybe is more suitable to create a Person repository and take the Typesetter using that repo istead of using the DbContext
            var typesetter = _dbContext.Typesetters.Single(t => t.UserId == command.TypesetterId);

						article.SetTypesetter(typesetter, command);

						article.SetStage(NextStage, command);

						await _articleRepository.SaveChangesAsync();

            await SendAsync( new ArticleCommandResponse(command.ArticleId));
        }
        protected override ArticleStage NextStage => ArticleStage.InProduction;
    }
}
