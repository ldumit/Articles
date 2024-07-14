using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.API.Features.AssignTypesetter
{
    [Authorize(Roles = "pof")]
    [HttpPut("{articleId:int}/typesetter")]
    public class AssignTypesetterEndpoint(IServiceProvider serviceProvider) 
        : BaseEndpoint<AssignTypesetterCommand, ArticleCommandResponse>(serviceProvider)
    {
        public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            using var transaction = _articleRepository.BeginTransaction();
            var article = _articleRepository.GetById(command.ArticleId);
            ChangeStage(article, command);

            article.TypesetterId = command.Body.UserId;
            await _articleRepository.SaveChangesAsync();

            //todo - transform into domain event
            await AddTypesetterToDiscussion(command.ArticleId, article);

            transaction.Commit();

            //await _workflowEventService.PublishEvent(new StartProductionEndWF1Event { ArticleId = command.ArticleId });
            //await _workflowEventService.PublishEvent(new StartProductionStartWF2Event { ArticleId = command.ArticleId });

            await SendAsync( new ArticleCommandResponse());
        }

        private async Task AddTypesetterToDiscussion(int articleId, Article article)
        {
            //var groups = await _discussionGroupRepository.GetGroupsByArticleIdAsync(articleId);
            //foreach (var group in groups)
            //{
            //    if (!group.Users.Any(x => x.UserId == (int)article.ArticleTypesetter.UserId))
            //        group.Users.Add(new GroupUser { UserId = (int)article.ArticleTypesetter.UserId });
            //}
            //await _discussionGroupRepository.SaveChangesAsync();
        }
        protected override ArticleStagesCode GetNextStage(Article article) => ArticleStagesCode.IN_PRODUCTION;
    }


    public class AssignTypesetterMapper : Mapper<AssignTypesetterCommand, ArticleCommandResponse, Typesetter>
    {
        //public override Typesetter ToEntity(AssignTypesetterCommand r) => new()
        //{
        //    UserId = r.Body.UserId,
        //    = DateOnly.Parse(r.BirthDay),
        //    FullName = $"{r.FirstName} {r.LastName}"
        //};

        //public override ArticleCommandResponse FromEntity(Typesetter e) => new()
        //{
        //    Id = e.Id,
        //    FullName = e.FullName,
        //    UserName = $"USR{e.Id:0000000000}",
        //    Age = (DateOnly.FromDateTime(DateTime.UtcNow).DayNumber - e.DateOfBirth.DayNumber) / 365,
        //};
    }
}
