using Review.Domain.Invitations.Enums;

namespace Review.Persistence.Repositories;

public class ReviewInvitationRepositoryy(ReviewDbContext dbContext)
		: Repository<ReviewInvitation>(dbContext)
{
		public async Task<ReviewInvitation> GetByTokenOrThrow(string token)
				=> await Query()
						.SingleOrThrowAsync(i => i.Token == token && i.Status == InvitationStatus.Open);
}
