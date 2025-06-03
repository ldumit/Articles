using Auth.Domain.Users.ValueObjects;
using Blocks.Core;
using EmailService.Contracts;

namespace Auth.Domain.Users;
public partial class User
{
		public static User Create(IUserCreationInfo userInfo)
		{
				if (userInfo.UserRoles.IsNullOrEmpty())
						throw new ArgumentException("User must have at least one role.", nameof(userInfo.UserRoles));

				var user = new User
				{
						UserName = userInfo.Email,
						Email = userInfo.Email,
						FirstName = userInfo.FirstName,
						LastName = userInfo.LastName,
						Gender = userInfo.Gender,
						PhoneNumber = userInfo.PhoneNumber,
						PictureUrl = userInfo.PictureUrl,
						Honorific = HonorificTitle.Create(userInfo.Honorific),
						ProfessionalProfile = ProfessionalProfile.Create(userInfo.Position, userInfo.CompanyName, userInfo.Affiliation),
						UserRoles = userInfo.UserRoles.Select(r => UserRole.Create(r.Type, r.StartDate, r.ExpiringDate)).ToList(),
						CreatedOn = DateTime.UtcNow
				};

				//user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, user.Name.FullName, user.RolesSnapshot()));

				return user;
		}

		public EmailMessage BuildConfirmationEmail(string resetLink, string fromEmailAddress)
		{
				const string ConfirmationEmail =
						@"Dear {0}, An account has been created for you. Please set your password using the following URL: {1}";

				return new EmailMessage(
						"Confirmation",
						new Content(ContentType.Html, string.Format(ConfirmationEmail, this.FullName, resetLink)),
						new EmailAddress("articles", fromEmailAddress),
						new List<EmailAddress> { new EmailAddress(this.FullName, this.Email!) }
						);
		}
}
