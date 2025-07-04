using Mapster;

namespace Submission.Domain.Entities;

public partial class Author
{
    public static Author Create(string email, string firstName, string lastName, string? honorific, string affiliation, IArticleAction action)
		{
				var author = new Author
				{
						Email = EmailAddress.Create(email),
						FirstName = firstName,
						LastName = lastName,
						Honorific = honorific,
						Affiliation = affiliation
				};

				var domainEvent = new AuthorCreated(author, action);
				author.AddDomainEvent(domainEvent);

				return author;
		}
}
