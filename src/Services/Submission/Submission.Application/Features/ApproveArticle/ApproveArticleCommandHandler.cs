using Auth.Grpc;
using Microsoft.EntityFrameworkCore;
using Submission.Persistence;
using static Auth.Grpc.AuthService;

namespace Submission.Application.Features.ApproveArticle;

public class ApproveArticleCommandHandler(ArticleRepository _articleRepository, PersonRepository _personRepository, ArticleStateMachineFactory _stateMachineFactory, AuthServiceClient _authClient)
		: IRequestHandler<ApproveArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(ApproveArticleCommand command, CancellationToken ct)
		{
				var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);

				var editor = await GetOrCreatePersonByUserId(command.CreatedById, _articleRepository.Context, command, ct);

				// todo - check the Journal Service if the editor is assigned to the article's journal (gRPC)

				article.Approve(editor, command, _stateMachineFactory);
				
				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}

		private async Task<Person> GetOrCreatePersonByUserId(int userId, SubmissionDbContext context, IArticleAction action, CancellationToken ct)
		{
				var person = await _articleRepository.Context.Persons.FirstOrDefaultAsync(x => x.UserId == userId, ct);
				if (person is null)
				{
						var response = _authClient.GetUserById(new GetUserRequest { UserId = userId });
						var userInfo = response.UserInfo;
						person = Person.Create(userInfo, action);
						await context.Persons.AddAsync(person, ct);
				}

				return person;
		}
}
