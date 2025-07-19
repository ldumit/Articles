using Microsoft.AspNetCore.Authorization;
using Journals.API.Features.Shared;
using Blocks.Exceptions;
using Articles.Security;
using Blocks.Redis;

namespace Journals.API.Features.Sections.Create;

[Authorize(Roles = Role.EOF)]
[HttpPost("journals/{journalId:int}/sections")]
[Tags("Sections")]
public class CreateSectionEndpoint(Repository<Journal> _repository)
    : Endpoint<CreateSectionCommand, IdResponse>
{
    public override async Task HandleAsync(CreateSectionCommand command, CancellationToken ct)
    {
        var journal = await _repository.GetByIdOrThrowAsync(command.JournalId);
        if(journal.Sections.Any(s => s.Name.EqualsIgnoreCase(command.Name)))
            throw new BadRequestException("Section with the same name already exists");

				var section = command.Adapt<Section>();
        //await _repository.SetNewId(section);
				section.Id = await _repository.GenerateNewId<Section>();
				journal.Sections.Add(section);

				await _repository.UpdateAsync(journal);

				await SendAsync(new IdResponse(section.Id));
    }
}
