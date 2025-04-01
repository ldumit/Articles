using Auth.Grpc;
using Microsoft.EntityFrameworkCore;


namespace Submission.Application.Features.CreateAndAssignAuthor;

public class CreateAndAssignAuthorCommandHandler(ArticleRepository _articleRepository, AuthService.AuthServiceClient _authClient)
		: IRequestHandler<CreateAndAssignAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var article = await _articleRepository.GetByIdOrThrowAsync(command.ArticleId);

				Author? author;
				if (command.UserId is null) // Author is not an User
						//author = command.Adapt<Author>();
						author = Author.Create(command.Email!, command.FirstName!, command.LastName!, command.Title, command.Affiliation!, command);
				else                        // Author is an User
						author = await CreateAuthorFromUser(_articleRepository, _authClient, command, ct);

				article.AssignAuthor(author, command.ContributionAreas, command.IsCorrespondingAuthor, command);

				await _articleRepository.SaveChangesAsync();

				return new IdResponse(article.Id);
		}

		private static async Task<Author> CreateAuthorFromUser(ArticleRepository _articleRepository, AuthService.AuthServiceClient _authClient, CreateAndAssignAuthorCommand command, CancellationToken ct)
		{
				var author = await _articleRepository.Context.Authors.FirstOrDefaultAsync(x => x.UserId == command.UserId!.Value, ct);
				if (author is null)
				{
						var response = _authClient.GetUserById(new GetUserRequest { UserId = command.UserId!.Value });
						var userInfo = response.UserInfo;
						author = Author.Create(userInfo.Email!, userInfo.FirstName!, userInfo.LastName!, userInfo.Title, userInfo.Affiliation!, command);

						//author = response.UserInfo.Adapt<Author>();
						await _articleRepository.Context.Authors.AddAsync(author, ct);
				}

				return author;
		}
}
