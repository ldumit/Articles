using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Production.Persistence.Repositories;
using Production.Domain.Entities;
using Production.Domain.Enums;
using Production.Persistence;

namespace Production.API.Features.AssignTypesetter
{
		[Authorize(Roles = "POF")]
    [HttpPut("articles/{articleId:int}/typesetter")]
    public class AssignTypesetterEndpoint(ProductionDbContext _dbContext, IServiceProvider serviceProvider) 
        : BaseEndpoint<AssignTypesetterCommand, ArticleCommandResponse>(serviceProvider)
    {
        public override async Task HandleAsync(AssignTypesetterCommand command, CancellationToken ct)
        {
            var article = _articleRepository.GetById(command.ArticleId);

						//var typesetter1 = _dbContext.Persons.Single(t => t.UserId == command.Body.UserId);
            //var types = typesetter as Typesetter;

            var typesetter = _dbContext.Typesetters.Single(t => t.UserId == command.Body.UserId);
						article.SetTypesetter(typesetter);
						article.SetStage(GetNextStage(article), command.ActionType, command.ActionComment, _claimsProvider.GetUserId(), null);

            article.Actors.Add(new ArticleActor() { PersonId = 1, Role = Articles.Security.UserRoleType.AUT });

						await _articleRepository.SaveChangesAsync();

            //todo - transform into domain event
            await AddTypesetterToDiscussion(command.ArticleId, article);

            await SendAsync( new ArticleCommandResponse(command.ArticleId));
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
        protected override ArticleStage GetNextStage(Article article) => ArticleStage.InProduction;
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

    public class AssignTypesetterCommandValidator : ArticleCommandValidator<UploadAuthorsProofCommand>
    {
        public AssignTypesetterCommandValidator(ArticleRepository articleRepository, AssetRepository assetRepository)
        {
        }
    }
}
