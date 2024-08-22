using Auth.Domain.GraphQL;
using Auth.Domain.Models;
using GraphQL;
using GraphQL.Types;

namespace Auth.Persistence.GraphQLQueries
{
		public class UserQuery : ObjectGraphType
		{
				public UserQuery(ApplicationDbContext dbContext)
				{
						Field<UserGraphType>(nameof(User))
								.Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }))
								.Resolve(context =>
								{
										var id = context.GetArgument<int>("id");
										return dbContext.Users.Find(id);
								}
						);
				}
		}

}
