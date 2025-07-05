using Auth.Grpc;
using Microsoft.EntityFrameworkCore;
using Submission.Persistence;

namespace Submission.Application.Features.ApproveArticle;

public class ApproveArticleCommandHandler(ArticleRepository _articleRepository, PersonRepository _personRepository, ArticleStateMachineFactory _stateMachineFactory, IPersonService _personClient)
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
						var response = await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest{ UserId = userId });
						var personInfo = response.PersonInfo;
						person = Person.Create(personInfo, action);
						await context.Persons.AddAsync(person, ct);
				}

				return person;
		}
}
