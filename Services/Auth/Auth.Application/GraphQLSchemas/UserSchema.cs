using Auth.Persistence.GraphQLQueries;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application.GraphQLSchemas;

public class UserSchema : Schema
{
		public UserSchema(IServiceProvider serviceProvider) : base(serviceProvider)
		{
				Query = serviceProvider.GetRequiredService<UserQuery>();
		}
}
