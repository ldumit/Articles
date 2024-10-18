using FastEndpoints;
using Mapster;
using Journals.Domain.Entities;
using Journals.API.Features.Shared;
using Articles.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Articles.Security;
using Articles.Redis;
using Journals.Persistence;

namespace Journals.API.Features.Journals.Create;

//talk about authorization, everyone who is authrnticated has the rights to call this endpoint?! 
[Authorize(Roles = Role.EOF)]
[HttpPost("journals")]
[Tags("Journals")]
public class CreateJournalEndpoint(JournalDbContext _dbContext, Repository<Journal> _journalRepository)
    : Endpoint<CreateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(CreateJournalCommand command, CancellationToken ct)
    {
        if (_journalRepository.Collection.Any(j =>j.Abbreviation == command.Abbreviation || j.NormalizedName == command.NormalizedName))
            throw new BadRequestException("Journal with the same name or abbreviation already exists");

        if(!await _dbContext.Editors.AnyAsync(e => e.Id == command.ChiefEditorId))
						throw new NotFoundException("Chief Editor cannot be found");

				var journal = command.Adapt<Journal>();

        await _journalRepository.AddAsync(journal);
        await _journalRepository.SaveAllAsync();

        await SendAsync(new IdResponse(journal.Id));
    }
}
