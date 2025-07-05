using Mapster;

namespace Review.Domain.Entities;

public partial class Reviewer
{
		public static Reviewer Create(string email, string firstName, string lastName, string? honorific, string affiliation, IArticleAction action)
		{
				var reviewer = new Reviewer
				{
						Email = EmailAddress.Create(email),
						FirstName = firstName,
						LastName = lastName,
						Honorific = honorific,
						Affiliation = affiliation
				};

				//todo - "with" creates another instance of the object
				var domainEvent = reviewer.Adapt<ReviewerCreated>() with { Action = action };
				reviewer.AddDomainEvent(domainEvent);

				return reviewer;
		}

		public void AddSpecialization(ReviewerSpecialization specialization)
		{
				if (!_specializations.Contains(specialization))
						_specializations.Add(specialization);
		}
}
