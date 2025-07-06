using Articles.Abstractions.Events.Dtos;
using Auth.Grpc;

namespace Review.Domain.Entities;

public partial class Author
{
		public static Author AcceptSubmitted(PersonInfo personInfo)
		{
				var person = new Author
				{
						Id = personInfo.Id,
						UserId = personInfo.UserId,
						Email = EmailAddress.Create(personInfo.Email),
						FirstName = personInfo.FirstName,
						LastName = personInfo.LastName,
						Honorific = personInfo.Honorific,
						Affiliation = personInfo.ProfessionalProfile?.Affiliation ?? string.Empty,						
				};

				return person;
		}
}
