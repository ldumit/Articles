using Blocks.Domain;

namespace Review.Domain.Entities;

public partial class ReviewInvitation
{
		public void Accept()
		{
				if (Status != InvitationStatus.Open)
						throw new DomainException("Invitation is not open anymore.");

				if(ExpiresOn <  DateTime.UtcNow)
						throw new DomainException("Invitation expired.");

				//todo consider adding an InvitationAccepted domain event
				Status = InvitationStatus.Accepted;
		}
}
