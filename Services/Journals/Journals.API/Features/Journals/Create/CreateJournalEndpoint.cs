using FastEndpoints;
using Mapster;
using Journals.Domain.Entities;
using Journals.Persistence.Repositories;
using Journals.API.Features.Shared;
using Articles.Exceptions;

namespace Journals.API.Features.Journals.Create;

//talk about authorization, everyone who is authrnticated has the rights to call this endpoint?! 
[HttpPost("journals")]
[Tags("Journals")]
public class CreateJournalEndpoint(Repository<Journal> _journalRepository)
    : Endpoint<CreateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(CreateJournalCommand command, CancellationToken ct)
    {
        if(_journalRepository.Collection.Any(j => j.Name == command.Name))
            throw new BadRequestException("Journal with the same name already exists");

        var journal = command.Adapt<Journal>();

        await _journalRepository.AddAsync(journal);
        await _journalRepository.SaveAllAsync();

        await SendAsync(new IdResponse(journal.Id));
    }
}
