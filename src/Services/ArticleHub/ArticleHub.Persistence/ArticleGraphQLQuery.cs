using ArticleHub.Domain;
using GraphQL;
using GraphQL.Client.Http;

namespace ArticleHub.Persistence;

public class QueryResult<T>
{
		public List<T> Items { get; init; } = new();
}

public class ArticleGraphQLQuery(GraphQLHttpClient client)
{
		private readonly GraphQLHttpClient _client = client;

		public async Task<QueryResult<Article>> GetArticlesAsync(string filters)
		{
				var query = new GraphQLRequest
				{
						Query = @"
                query GetArticles($filter: article_bool_exp) {
                    articles(where: $filter) {
                        id
                        title
                        doi
                        stage
                        journal {
                            id
                            abbreviation
                            name
                        }
                        submitedBy {
                            firstName
                            lastName
                            email
                        }
                        submitedOn
                        acceptedOn
                        publishedOn
                    }
                }",

						Variables = new { filter = filters }
				};


				var response = await _client.SendQueryAsync<QueryResult<Article>>(query);
				return response.Data;
		}

		public async Task<QueryResult<Article>> GetArticlesAsync(Dictionary<string, object?> filters)
		{
				string filterQuery = BuildFilterQuery(filters);

				return await GetArticlesAsync(filterQuery);

		}

		private string BuildFilterQuery(Dictionary<string, object?>? filters)
		{
				if (filters == null || filters.Count == 0)
				{
						return "{}";
				}

				var filterParts = filters
						.Select(kv => $"{kv.Key}: {kv.Value}")
						.Aggregate((current, next) => $"{current}, {next}");

				return $"{{ {filterParts} }}";
		}
}

