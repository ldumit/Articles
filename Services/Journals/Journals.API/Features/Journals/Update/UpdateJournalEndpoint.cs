using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Mapster;
using Journals.Domain.Entities;
using Journals.Persistence.Repositories;
using Journals.API.Features.Shared;

namespace Journals.API.Features.Journals.Update;


[Authorize(Roles = "POF")]
[HttpPut("journals/{id:int}")]
[Tags("Journals")]
public class UpdateJournalEndpoint(Repository<Journal> _journalRepository)
    : Endpoint<UpdateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(UpdateJournalCommand command, CancellationToken ct)
    {
				var journal = await _journalRepository.GetByIdOrThrowAsync(command.Id);
				command.Adapt(journal);

        await _journalRepository.UpdateAsync(journal);

        await SendAsync(new IdResponse(journal.Id));
    }
}
