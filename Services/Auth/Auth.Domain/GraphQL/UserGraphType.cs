using Auth.Domain.Models;
using GraphQL.Types;

namespace Auth.Domain.GraphQL;

public class UserGraphType : ObjectGraphType<User>
{
    public UserGraphType()
    {
				Field(x => x.Id).Description("The ID of the User.");
				Field(x => x.FirstName).Description("The FirstName of the User.");
				Field(x => x.LastName).Description("The FirstName of the User.");
				Field(x => x.CompanyName);
				Field(x => x.UserRoles).Description("The roles of the user");
				Field(x => x.Email);
				Field(x => x.PhoneNumber);
				Field(x => x.Position);
				Field(x => x.PictureUrl);
		}
}
