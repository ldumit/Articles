using Auth.Grpc;
using Blocks.Core;
using Mapster;

namespace Review.Domain.Entities;

public partial class Reviewer
{
		public static Reviewer Create(PersonInfo personInfo, IEnumerable<int> journalIds, IArticleAction action)
		{
				if(journalIds.IsNullOrEmpty())
						throw new ArgumentNullException("A reviewer must have at least one specialization");		

				var reviewer = new Reviewer
				{
						Id = personInfo.Id,
						UserId = personInfo.UserId,
						Email = EmailAddress.Create(personInfo.Email),
						FirstName = personInfo.FirstName,
						LastName = personInfo.LastName,
						Honorific = personInfo.Honorific,
						Affiliation = personInfo.ProfessionalProfile!.Affiliation,
						CreatedById = action.CreatedById,
						CreatedOn = DateTime.UtcNow
				};

				reviewer._specializations = [.. journalIds.Select(journalId => new ReviewerSpecialization { JournalId = journalId, ReviewerId = reviewer.Id })];

				var domainEvent = new ReviewerCreated(reviewer, action);
				reviewer.AddDomainEvent(domainEvent);

				return reviewer;
		}

		public void AddSpecialization(ReviewerSpecialization specialization)
		{
				if (!_specializations.Contains(specialization))
						_specializations.Add(specialization);
		}
}
