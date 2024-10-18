using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Mapster;
using Journals.Domain.Entities;
using Journals.API.Features.Shared;
using Articles.Security;
using Articles.Redis;
using Journals.Domain.Events;

namespace Journals.API.Features.Journals.Update;

[Authorize(Roles = Role.EOF)]
[HttpPut("journals/{journalId:int}")]
[Tags("Journals")]
public class UpdateJournalEndpoint(Repository<Journal> _journalRepository)
    : Endpoint<UpdateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(UpdateJournalCommand command, CancellationToken ct)
    {
				var journal = await _journalRepository.GetByIdOrThrowAsync(command.JournalId);
				command.Adapt(journal);

        await _journalRepository.UpdateAsync(journal);

				await PublishAsync(journal.Adapt<UpdateJournalEvent>());

				await SendAsync(new IdResponse(journal.Id));
    }
}
