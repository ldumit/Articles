using Microsoft.AspNetCore.Authorization;
using FastEndpoints;
using Mapster;
using Articles.Security;
using Blocks.Redis;
using Blocks.Linq;
using Journals.API.Features.Shared;
using Journals.Domain.Journals;
using Journals.Domain.Journals.Events;

namespace Journals.API.Features.Sections.Update;

[Authorize(Roles = Role.EditorAdmin)]
[HttpPut("journals/{journalId:int}/sections/{sectionId:int}")]
[Tags("Sections")]
public class UpdateSectionEndpoint(Repository<Journal> _repository)
    : Endpoint<UpdateSectionCommand, IdResponse>
{
    public override async Task HandleAsync(UpdateSectionCommand command, CancellationToken ct)
    {
				var journal = await _repository.GetByIdOrThrowAsync(command.JournalId);
				var section = journal.Sections.SingleOrThrow(s => s.Id == command.SectionId);
				section = command.Adapt<Section>();

				await _repository.UpdateAsync(journal);

				await PublishAsync(section.Adapt<SectionUpdated>());

				await Send.OkAsync(new IdResponse(section.Id));
    }
}
