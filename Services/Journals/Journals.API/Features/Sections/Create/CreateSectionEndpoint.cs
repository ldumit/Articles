using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Mapster;
using Journals.Domain.Entities;
using Journals.API.Features.Shared;
using Articles.Exceptions;
using Journals.Persistence.Repositories;

namespace Journals.API.Features.Sections.Create;

[Authorize(Roles = "POF")]
[HttpPost("journals/{journalId:int}/sections")]
[Tags("Sections")]
public class CreateSectionEndpoint(Repository<Journal> _repository)
    : Endpoint<CreateSectionCommand, IdResponse>
{
    public override async Task HandleAsync(CreateSectionCommand command, CancellationToken ct)
    {
        var journal = await _repository.GetByIdOrThrowAsync(command.JournalId);
        if(journal.Sections.Any(s => s.Name == command.Name))
            throw new BadRequestException("Section with the same name already exists");

				var section = command.Adapt<Section>();
        //await _repository.SetNewId(section);
				section.Id = await _repository.GenerateNewId<Section>();
				journal.Sections.Add(section);

				await _repository.UpdateAsync(journal);
				await _repository.SaveAllAsync();

				await SendAsync(new IdResponse(section.Id));
    }
}
