using GraphQL;
using GraphQL.Client.Http;
using Blocks.Core.GraphQL;
using ArticleHub.Domain.Dtos;

namespace ArticleHub.Persistence;

public class ArticleGraphQLQuery(GraphQLHttpClient client)
{
		private readonly GraphQLHttpClient _client = client;

		public async Task<QueryResult<ArticleDto>> GetArticlesAsync(object filters)
		{
				//todo - build an ednpoint that will return metadata about articles so it can be used to filter articles
				var query = new GraphQLRequest
				{
						Query = @"
                query GetArticles($filter: article_bool_exp) {
                    items:article(where: $filter)  {
												id
												title    
												doi
												stage
												submittedon
												publishedon
												acceptedon
												journal {
													abbreviation
													name
												}
												submittedby:person {
													email
													firstname
													lastname
												}
										}
                }",

						Variables = new { filter = filters }
				};


				var response = await _client.SendQueryAsync<QueryResult<ArticleDto>>(query);
				return response.Data;
		}

		public async Task<QueryResult<ArticleDto>> GetArticlesAsync(Dictionary<string, object?> filters)
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

