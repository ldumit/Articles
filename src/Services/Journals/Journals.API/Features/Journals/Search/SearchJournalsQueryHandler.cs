using Microsoft.AspNetCore.Authorization;
using Redis.OM;
using Blocks.Redis;
using Articles.Security;
using Journals.API.Features.Shared;

namespace Journals.API.Features.Journals.Search;

[Authorize(Roles = Role.EditorAdmin)]
[HttpGet("journals")]
[Tags("Journals")]
public class SearchJournalsQueryHandler(Repository<Journal> _repository, Repository<Editor> _editorRepository)
		: Endpoint<SearchJournalsQuery, SearchJournalsResponse>
{
		public override async Task HandleAsync(SearchJournalsQuery query, CancellationToken ct)
		{
				var collection = _repository.Collection;

				if (!string.IsNullOrWhiteSpace(query.Search))
				{
						var search = query.Search.Trim().ToLowerInvariant();
						var queryString =
								$"(@Abbreviation:{{{search}}}) | " +
								$"(@Name:*{search}* | @Description:*{search}*)";

						collection = collection.Raw(queryString);
				}

				var totalCount = collection.Count();

				var items = collection
						.OrderBy(j => j.NormalizedName)
						.Skip((query.Page - 1) * query.PageSize)
						.Take(query.PageSize)
						.ToList();

				var response = new SearchJournalsResponse(
						query.Page,
						query.PageSize,
						totalCount,
						items.Select(i =>
						{
								var dto = i.Adapt<JournalDto>();
								dto.ChiefEditor = _editorRepository.GetById(i.ChiefEditorId).Adapt<EditorDto>();
								return dto;
					  })
				);

				await Send.OkAsync(response, cancellation: ct);
		}
}