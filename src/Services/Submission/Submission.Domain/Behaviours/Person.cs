using Auth.Grpc;

namespace Submission.Domain.Entities;

public partial class Person
{
		public static Person Create(int? userId, string email, string firstName, string lastName, string? title, string affiliation, IArticleAction action)
		{
				return new Person
				{
						UserId = userId,
						Email = EmailAddress.Create(email),
						FirstName = firstName,
						LastName = lastName,
						Title = title,
						Affiliation = affiliation
				};
		}

		public static Person Create(UserInfo userInfo, IArticleAction action)
		{
				return new Person
				{
						UserId = userInfo.Id,
						Email = EmailAddress.Create(userInfo.Email),
						FirstName = userInfo.FirstName,
						LastName = userInfo.LastName,
						Title = userInfo.Title,
						Affiliation = userInfo.Affiliation
				};
		}
}
