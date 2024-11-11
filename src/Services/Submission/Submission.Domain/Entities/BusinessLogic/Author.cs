using Articles.Abstractions;
using Mapster;
using Submission.Domain.Events;
using Submission.Domain.ValueObjects;

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

				//todo - "with" creates another instance of the object
				var domainEvent = author.Adapt<AuthorCreated>() with { Action = action };
				author.AddDomainEvent(domainEvent);

				return author;
		}
}
