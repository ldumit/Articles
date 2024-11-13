using FastEndpoints;
using Mapster;
using Journals.Persistence;
using Journals.API.Features.Shared;
using Blocks.Redis;

namespace Journals.API.Features.Journals.Get;

[HttpGet("journals/{journalId:int}")]
[Tags("Journals")]
public class GetJournalEndpoint(JournalDbContext _dbContext)
    : Endpoint<GetJournalQuery, GetJournalResponse>
{
    public override async Task HandleAsync(GetJournalQuery query, CancellationToken ct)
    {
        var journal = await _dbContext.Journals.GetByIdOrThrowAsync(query.JournalId);

        var journalDto = journal.Adapt<JournalDto>();

        var chiefEditor = await _dbContext.Editors.GetByIdAsync(journal.ChiefEditorId);
        journalDto.ChiefEditor = chiefEditor.Adapt<EditorDto>();

        await SendAsync(new GetJournalResponse(journalDto));
    }
}
