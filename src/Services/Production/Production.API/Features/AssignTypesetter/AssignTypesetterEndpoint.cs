using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Articles.Abstractions.Enums;
using Production.API.Features.Shared;
using Production.Application.StateMachines;
using Production.Domain.Enums;
using Articles.Security;
using Articles.EntityFrameworkCore;
using Articles.Exceptions;
using Articles.Abstractions;

namespace Production.API.Features.AssignTypesetter
{
		[Authorize(Roles = Role.POF)]
    [HttpPut("articles/{articleId:int}/typesetter/{typesetterId:int}")]
		[Tags("Articles")]
		public class AssignTypesetterEndpoint(ArticleRepository articleRepository, ArticleStateMachineFactory _stateMachineFactory) 
        : BaseEndpoint<AssignTypesetterCommand, IdResponse>(articleRepository)
    {
				public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            _article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);
						
						// todo move transisiton check to the domain ?!?
						CheckAndThrowStageTransition(command.ActionType);

						var typesetter = await _articleRepository.Context.Typesetters.FindByIdOrThrowAsync(command.TypesetterId);						

						_article.AssignTypesetter(typesetter, command);

						_article.SetStage(NextStage, command);

						await _articleRepository.SaveChangesAsync();

            await SendAsync( new IdResponse(command.ArticleId));
        }
        protected override ArticleStage NextStage => ArticleStage.InProduction;
				protected virtual void CheckAndThrowStageTransition(ArticleActionType actionType)
				{
						if (!_stateMachineFactory(_article.Stage).CanFire(actionType))
								throw new BadRequestException("Action not allowed");
				}
		}
}
