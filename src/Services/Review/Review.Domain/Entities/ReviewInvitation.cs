﻿using System.Runtime;

namespace Review.Domain.Entities;

public partial class ReviewInvitation : AggregateEntity
{
    public required int ArticleId { get; init; }

		public int? UserId { get; init; }
		public required string EmailAddress { get; init; }
		public required string FullName { get; init; }

		public DateTime SentOn { get; init; } = DateTime.UtcNow;
		public required int SentById { get; set; }
		public Person SentBy { get; set; } = null!;

		public required DateTime ExpiresOn { get; init; }
		public bool IsExpired => ExpiresOn < DateTime.UtcNow;

		public required string Token { get; set; }

    public InvitationStatus Status { get; set; } = InvitationStatus.Open;
}
