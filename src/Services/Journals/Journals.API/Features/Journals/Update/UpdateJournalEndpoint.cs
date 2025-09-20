using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Mapster;
using Journals.API.Features.Shared;
using Articles.Security;
using Blocks.Redis;
using Journals.Domain.Journals;
using Journals.Domain.Journals.Events;

namespace Journals.API.Features.Journals.Update;

[Authorize(Roles = Role.EditorAdmin)]
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

				await Send.OkAsync(new IdResponse(journal.Id));
    }
}
