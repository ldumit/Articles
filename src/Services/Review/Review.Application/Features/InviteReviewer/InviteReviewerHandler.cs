using Microsoft.EntityFrameworkCore;

namespace Review.Application.Features.InviteReviewer;

public class InviteReviewerHandler(Repository<Journal> _journalRepository)
		: IRequestHandler<InviteReviewerCommand, IdResponse>
{
		public async Task<IdResponse> Handle(InviteReviewerCommand command, CancellationToken ct)
		{
				var journal = await _journalRepository.FindByIdOrThrowAsync(command.JournalId);

				var article = journal.InviteReviewer(command.Title, command.Type, command.Scope, command.JournalId, command);

				await AssignCurrentUserAsAuthor(article, command);

				await _journalRepository.SaveChangesAsync(ct);

				return new IdResponse(article.Id);
		}

		private async Task AssignCurrentUserAsAuthor(Article article, InviteReviewerCommand command)
		{
				var author = await _journalRepository.Context.Authors.SingleOrDefaultAsync(t => t.UserId == command.CreatedById);
				if (author is not null)
						article.AssignAuthor(author, [ContributionArea.OriginalDraft], isCorrespondingAuthor: true, command);
		}
}
