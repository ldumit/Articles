using Microsoft.AspNetCore.Authorization;
using FastEndpoints;
using Mapster;
using Articles.Security;
using Journals.API.Features.Shared;
using Journals.Persistence;

namespace Journals.API.Features.Journals.Search;

[Authorize(Roles = Role.EOF)]
[HttpGet("journals")]
[Tags("Journals")]
public class SearchJournalsQueryHandler(JournalDbContext _dbContext)
		: Endpoint<SearchJournalsQuery, SearchJournalsResponse>
{
		public override async Task HandleAsync(SearchJournalsQuery query, CancellationToken ct)
		{
				var collection = _dbContext.Journals;

				// Base query
				var journalsQuery = collection.AsQueryable();

				if (!string.IsNullOrWhiteSpace(query.Search))
				{
						var search = query.Search.ToLowerInvariant();
						journalsQuery = journalsQuery.Where(j =>
								j.NormalizedName.Contains(search) ||
								j.Abbreviation.ToLower().Contains(search));
				}

				var totalCount = journalsQuery.Count();

				var items = journalsQuery
						.OrderBy(j => j.Name)
						.Skip((query.Page - 1) * query.PageSize)
						.Take(query.PageSize)
						.ProjectToType<JournalDto>()
						.ToList();

				var response = new SearchJournalsResponse(
						query.Page,
						query.PageSize,
						totalCount,
						items
				);

				await SendAsync(response, cancellation: ct);
		}
}