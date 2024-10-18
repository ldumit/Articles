using FastEndpoints;
using Mapster;
using Journals.Persistence;
using Journals.API.Features.Shared;
using Journals.API.Features.Editors.Get;
using Articles.Exceptions;

namespace Journals.API.Features.Editors.GetById;

//[Authorize(Roles = "POF")]
[HttpGet("journals/{journalId:int}/sections/{sectionId:int}/editors")]
[Tags("Sections")]
public class GetEditorsBySectionIdEndpoint(JournalDbContext _dbContext)
    : Endpoint<GetEditorsBySectionIdQuery, GetEditorsBySectionIdResponse>
{
    public override async Task HandleAsync(GetEditorsBySectionIdQuery query, CancellationToken ct)
    {
        var journal = await _dbContext.Journals.GetByIdOrThrowAsync(query.JournalId);
        var section = journal.Sections.FirstOrDefault(s => s.Id == query.SectionId);
        if (section == null) throw new NotFoundException("Section not found");
        var sectionDto = section.Adapt<SectionDto>();
        
        List<EditorDto> editors = new();
        foreach (var editorRole in section.EditorRoles)
        {
            var editor = await _dbContext.Editors.GetByIdAsync(editorRole.EditorId);
            editors.Add(editor.Adapt<EditorDto>());
        }

        await SendAsync(new GetEditorsBySectionIdResponse(editors));
    }
}
