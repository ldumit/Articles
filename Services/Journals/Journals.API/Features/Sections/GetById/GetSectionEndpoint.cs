using FastEndpoints;
using Mapster;
using Journals.Persistence;
using Journals.API.Features.Shared;
using Articles.Redis;

namespace Journals.API.Features.Sections.GetById;

[HttpGet("journals/{journalId:int}/sections/{sectionId:int}")]
[Tags("Sections")]
public class GetSectionEndpoint(JournalDbContext _dbContext)
    : Endpoint<GetSectionQuery, GetSectionResponse>
{
    public override async Task HandleAsync(GetSectionQuery query, CancellationToken ct)
    {
        var journal = await _dbContext.Journals.GetByIdOrThrowAsync(query.JournalId);
				var section = journal.Sections.SingleOrThrow(s => s.Id == query.SectionId);

        var sectionDto = section.Adapt<SectionDto>();

        foreach (var editorRole in section.EditorRoles.Take(4))
        {
            var editor = await _dbContext.Editors.GetByIdAsync(editorRole.EditorId);
            sectionDto.Editors.Add(editor.Adapt<EditorDto>());
        }

        await SendAsync(new GetSectionResponse(sectionDto));
    }
}
