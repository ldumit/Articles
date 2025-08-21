using FastEndpoints;
using Mapster;
using Blocks.Redis;
using Blocks.Linq;
using Journals.Persistence;
using Journals.API.Features.Shared;
using Journals.API.Features.Editors.Get;

namespace Journals.API.Features.Editors.GetById;

[HttpGet("journals/{journalId:int}/sections/{sectionId:int}/editors")]
[Tags("Sections")]
public class GetEditorsBySectionIdEndpoint(JournalDbContext _dbContext)
    : Endpoint<GetEditorsBySectionIdQuery, GetEditorsBySectionIdResponse>
{
    public override async Task HandleAsync(GetEditorsBySectionIdQuery query, CancellationToken ct)
    {
        var journal = await _dbContext.Journals.GetByIdOrThrowAsync(query.JournalId);
        var section = journal.Sections.SingleOrThrow(s => s.Id == query.SectionId);

        var sectionDto = section.Adapt<SectionDto>();
        
        var editors = new List<EditorDto>();
        foreach (var editorRole in section.EditorRoles)
        {
            var editor = await _dbContext.Editors.GetByIdAsync(editorRole.EditorId);
            editors.Add(editor.Adapt<EditorDto>());
        }

        await Send.OkAsync(new GetEditorsBySectionIdResponse(editors));
    }
}
