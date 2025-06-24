using Blocks.Domain;

namespace Review.Domain.Entities;

public partial class ReviewInvitation
{
		public void Accept()
		{
				if (Status != InvitationStatus.Open)
						throw new DomainException("Invitation is not open anymore.");

				Status = InvitationStatus.Accepted;
		}
}
