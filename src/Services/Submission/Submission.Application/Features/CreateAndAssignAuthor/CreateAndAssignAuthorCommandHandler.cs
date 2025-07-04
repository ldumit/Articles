using Auth.Grpc;
using Microsoft.EntityFrameworkCore;
using static Auth.Grpc.AuthService;


namespace Submission.Application.Features.CreateAndAssignAuthor;

public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository, AuthServiceClient _authClient)
		: IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				Author? author;
				if (command.UserId is null) // Author is not an User
						author = Author.Create(command.Email!, command.FirstName!, command.LastName!, command.Title, command.Affiliation!, command);
				else                        // Author is an User
						author = await CreateAuthorFromUser(command, ct);

				article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor, command);

				await _articleRepository.SaveChangesAsync(ct);

				return new IdResponse(article.Id);
		}

		// This logic may be extracted into AuthorFactory if reused later (that's the reason for being static)
		private async Task<Author> CreateAuthorFromUser(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var author = await _articleRepository.Context.Authors.FirstOrDefaultAsync(x => x.UserId == command.UserId!.Value, ct);
				if (author is null)
				{
						var response = _authClient.GetUserById(new GetUserRequest { UserId = command.UserId!.Value });
						var userInfo = response.UserInfo;
						author = Author.Create(userInfo.Email, userInfo.FirstName, userInfo.LastName, userInfo.Honorific, userInfo.Affiliation, command);
						// or if you preffer a simpler aproach you can just use Adapt
						//author = response.UserInfo.Adapt<Author>();
						await _articleRepository.Context.Authors.AddAsync(author, ct);
				}

				return author;
		}
}
