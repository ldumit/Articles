using Review.Domain.Invitations.Enums;

namespace Review.Persistence.Repositories;

public class ReviewInvitationRepository(ReviewDbContext dbContext)
		: Repository<ReviewInvitation>(dbContext)
{
		public async Task<ReviewInvitation> GetByTokenOrThrow(string token, CancellationToken ct = default)
				=> await Query()
						.SingleOrThrowAsync(i => i.Token.Value == token && i.Status == InvitationStatus.Open, ct);

		public async Task<List<ReviewInvitation>> GetByArticleIdAsync(int articleId, CancellationToken ct = default)
				=> await Query()
						.Where(x => x.ArticleId == articleId)
						.ToListAsync(ct);
}
