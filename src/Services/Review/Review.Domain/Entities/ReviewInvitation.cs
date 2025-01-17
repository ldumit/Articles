using System.Runtime;

namespace Review.Domain.Entities;

public class ReviewInvitation : AggregateEntity
{
    public required int ArticleId { get; init; }
    public required string EmailAddress { get; init; }
		public required string FullName { get; init; }

		public DateTime SentOn { get; init; } = DateTime.UtcNow;
		public required int SentById { get; set; }
		public Person SentBy { get; set; } = null!;

		public required DateTime ExpiresOn { get; init; }

    public required string Token { get; set; }

    public InvitationStatus Status { get; set; } = InvitationStatus.Open;
}
