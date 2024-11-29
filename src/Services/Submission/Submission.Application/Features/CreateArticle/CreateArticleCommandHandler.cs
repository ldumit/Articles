using Microsoft.EntityFrameworkCore;

namespace Submission.Application.Features.CreateArticle;

public class CreateArticleCommandHandler(Repository<Journal> _journalRepository)
		: IRequestHandler<CreateArticleCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateArticleCommand command, CancellationToken ct)
		{
				var journal = await _journalRepository.FindByIdOrThrowAsync(command.JournalId);

				var article = journal.CreateArticle(command.Title, command.Type, command.Scope, command.JournalId, command);

				await AssignCurrentUserAsAuthor(article, command);

				await _journalRepository.SaveChangesAsync(ct);

				return new IdResponse(article.Id);
		}

		private async Task AssignCurrentUserAsAuthor(Article article, CreateArticleCommand command)
		{
				var author = await _journalRepository.Context.Authors.SingleOrDefaultAsync(t => t.UserId == command.CreatedById);
				if (author is not null)
						article.AssignAuthor(author, [ContributionArea.OriginalDraft], isCorrespondingAuthor: true, command);
		}
}
