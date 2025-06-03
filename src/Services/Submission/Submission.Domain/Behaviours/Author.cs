using Mapster;

namespace Submission.Domain.Entities;

public partial class Author
{
    public static Author Create(string email, string firstName, string lastName, string? title, string affiliation, IArticleAction action)
		{
				var author = new Author
				{
						Email = EmailAddress.Create(email),
						FirstName = firstName,
						LastName = lastName,
						Title = title,
						Affiliation = affiliation
				};

				var domainEvent = new AuthorCreated(author, action);
				author.AddDomainEvent(domainEvent);

				return author;
		}
}
