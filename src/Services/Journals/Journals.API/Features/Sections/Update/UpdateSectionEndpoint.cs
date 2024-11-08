using FastEndpoints;
using Mapster;
using Journals.Domain.Entities;
using Journals.API.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Articles.Security;
using Articles.Redis;
using Journals.Domain.Events;

namespace Journals.API.Features.Sections.Update;

[Authorize(Roles = Role.EOF)]
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

				await PublishAsync(section.Adapt<UpdateSectionEvent>());

				await SendAsync(new IdResponse(section.Id));
    }
}
