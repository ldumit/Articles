namespace Auth.Domain.Models;
public partial class User
{
		public static User Create(string email, string firstName, string lastName, Gender gender)
		{
				return new User
				{
						Email = email,
						FirstName = firstName,
						LastName = lastName,
						Gender = gender
				};
		}
}
