using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.Persistence;
using Articles.Abstractions.Enums;
using Production.API.Features.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Articles.Security;
using Articles.EntityFrameworkCore;
using Articles.Exceptions;

namespace Production.API.Features.AssignTypesetter
{
		[Authorize(Roles = Role.POF)]
    [HttpPut("articles/{articleId:int}/typesetter/{typesetterId:int}")]
		[Tags("Articles")]
		public class AssignTypesetterEndpoint(ArticleRepository articleRepository, ProductionDbContext _dbContext, ArticleStateMachineFactory _stateMachineFactory) 
        : BaseEndpoint<AssignTypesetterCommand, ArticleCommandResponse>(articleRepository)
    {
				public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            _article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
						CheckAndThrowStageTransition(command.ActionType);

						//todo - maybe is more suitable to create a Person repository and take the Typesetter using that repo istead of using the DbContext
						var typesetter = _dbContext.Typesetters.Single(t => t.UserId == command.TypesetterId);

						_article.SetTypesetter(typesetter, command);

						_article.SetStage(NextStage, command);
						await _articleRepository.SaveChangesAsync();

            await SendAsync( new ArticleCommandResponse(command.ArticleId));
        }
        protected override ArticleStage NextStage => ArticleStage.InProduction;
				protected virtual void CheckAndThrowStageTransition(ArticleActionType actionType)
				{
						if (!_stateMachineFactory(_article.Stage).CanFire(actionType))
								throw new BadRequestException("Action not allowed");
				}
		}
}
