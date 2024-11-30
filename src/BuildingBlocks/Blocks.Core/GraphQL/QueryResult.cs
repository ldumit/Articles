namespace Blocks.Core.GraphQL;

public class QueryResult<T>
{
		public List<T> Items { get; init; } = new();
}
