using FastEndpoints;
using Mapster;
using Journals.Domain.Entities;
using Journals.API.Features.Shared;
using Journals.Persistence.Repositories;
using Articles.Exceptions;

namespace Journals.API.Features.Sections.Update;

[HttpPost("journals/{journalId:int}/sections/{sectionId:int}")]
[Tags("Sections")]
public class UpdateSectionEndpoint(Repository<Journal> _repository)
    : Endpoint<UpdateSectionCommand, IdResponse>
{
    public override async Task HandleAsync(UpdateSectionCommand command, CancellationToken ct)
    {
				var journal = await _repository.GetByIdOrThrowAsync(command.JournalId);
				var section = journal.Sections.SingleOrDefault(s => s.Id == command.Id);
				if(section is null)
						throw new NotFoundException("Section not found");

				section = command.Adapt<Section>();

				await _repository.UpdateAsync(journal);

				await SendAsync(new IdResponse(section.Id));
    }
}
