using FastEndpoints;
using Mapster;
using Journals.Persistence;
using Journals.API.Features.Shared;
using Articles.Exceptions;

namespace Journals.API.Features.Sections.GetById;

//[Authorize(Roles = "POF")]
[HttpGet("journals/{journalId:int}/sections/{sectionId:int}")]
[Tags("Articles")]
public class GetSectionEndpoint(JournalDbContext _dbContext)
    : Endpoint<GetSectionQuery, GetSectionResponse>
{
    public override async Task HandleAsync(GetSectionQuery query, CancellationToken ct)
    {
        var journal = await _dbContext.Journals.GetByIdOrThrowAsync(query.JournalId);
        var section = journal.Sections.FirstOrDefault(s => s.Id == query.SectionId);
        if (section == null) throw new NotFoundException("Section not found");

        var sectionDto = section.Adapt<SectionDto>();

        foreach (var editorRole in section.EditorRoles.Take(4))
        {
            var editor = await _dbContext.Editors.GetByIdAsync(editorRole.EditorId);
            sectionDto.Editors.Add(editor.Adapt<EditorDto>());
        }

        await SendAsync(new GetSectionResponse(sectionDto));
    }
}
