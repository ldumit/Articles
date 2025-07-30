using Auth.Grpc;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Submission.Application.Features.CreateAndAssignAuthor;

public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository, IPersonService _personClient)
		: IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				Author? author;
				if (command.PersonId == null) //new author, new person
				{
						var personInfo = await CreatePersonAsync(command, ct);
						author = Author.Create(personInfo, command);
				}
				else
				{
						author = await _articleRepository.Context.Authors.SingleOrDefaultAsync(x => x.Id == command.PersonId, ct);
						if (author is null)
						{
								var personInfo = await GetPersonAsync(command, ct);
								author = Author.Create(personInfo, command);
						}
				}

				article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor, command);

				await _articleRepository.SaveChangesAsync(ct);

				return new IdResponse(article.Id);
		}

		private async Task<PersonInfo> GetPersonAsync(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var response = await _personClient.GetPersonByIdAsync(new GetPersonRequest { PersonId = command.PersonId!.Value }, new CallOptions(cancellationToken: ct));
				return response.PersonInfo;
		}

		private async Task<PersonInfo> CreatePersonAsync(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var createPersonRequest = command.Adapt<CreatePersonRequest>();
				var response = await _personClient.CreatePersonAsync(createPersonRequest, new CallOptions(cancellationToken: ct));
				return response.PersonInfo;
		}
}
