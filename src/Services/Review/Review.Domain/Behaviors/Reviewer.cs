using Mapster;

namespace Review.Domain.Entities;

public partial class Reviewer
{
		public static Reviewer Create(string email, string firstName, string lastName, string? title, string affiliation, IArticleAction action)
		{
				var reviewer = new Reviewer
				{
						Email = EmailAddress.Create(email),
						FirstName = firstName,
						LastName = lastName,
						Title = title,
						Affiliation = affiliation
				};

				//todo - "with" creates another instance of the object
				var domainEvent = reviewer.Adapt<ReviewerCreated>() with { Action = action };
				reviewer.AddDomainEvent(domainEvent);

				return reviewer;
		}

		public void AddSpecialization(Journal journal)
		{
				//if (!_specializations.Contains(journal))
				//		_specializations.Add(journal);
		}
}
