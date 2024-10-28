using MediatR;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;

namespace Submission.Application.Features.CreateArticle;

public class CreateArticleCommandHandler(Repository<Journal> _journalRepository) 
		: IRequestHandler<CreateArticleCommand, CreateArticleResponse>
{
		public async Task<CreateArticleResponse> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
		{
				var journal = await _journalRepository.FindByIdAsync(command.JournalId, throwNotFound: true);

				var article = journal.CreateArticle(command.Title, command.Type, command.ScopeStatement, command.JournalId, command);

				await _journalRepository.SaveChangesAsync();

				return new CreateArticleResponse(article.Id);
		}
}
