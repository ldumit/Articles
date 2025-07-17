using Articles.Security;
using Auth.Grpc;
using Blocks.Exceptions;
using Blocks.Redis;
using FastEndpoints;
using Grpc.Core;
using Journals.API.Features.Shared;
using Journals.Domain.Journals;
using Journals.Persistence;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Journals.API.Features.Journals.Create;

//talk about authorization, everyone who is authenticated has the rights to call this endpoint?! 
[Authorize(Roles = Role.EOF)]
[HttpPost("journals")]
[Tags("Journals")]
public class CreateJournalEndpoint(JournalDbContext _dbContext, Repository<Journal> _journalRepository, Repository<Editor> _editorRepository, IPersonService _personClient)
    : Endpoint<CreateJournalCommand, IdResponse>
{
    public override async Task HandleAsync(CreateJournalCommand command, CancellationToken ct)
    {
        if (_journalRepository.Collection.Any(j =>j.Abbreviation == command.Abbreviation || j.NormalizedName == command.NormalizedName))
            throw new BadRequestException("Journal with the same name or abbreviation already exists");

        if (!_editorRepository.Collection.Any(e => e.Id == command.ChiefEditorId))
            await CreateEditor(command.ChiefEditorId, ct);
						//throw new NotFoundException("Chief Editor cannot be found");

				var journal = command.Adapt<Journal>();

        await _journalRepository.AddAsync(journal);
        await _journalRepository.SaveAllAsync();

        await SendAsync(new IdResponse(journal.Id));
    }

		private async Task<Editor> CreateEditor(int userId, CancellationToken ct)
		{
				var response = await _personClient.GetPersonByUserIdAsync( new GetPersonByUserIdRequest { UserId = userId }, new CallOptions(cancellationToken: ct));
        var personInfo = response.PersonInfo;
				var editor = new Editor
				{
						Id = personInfo.UserId!.Value,
						PersonId = personInfo.Id,
						Affiliation = personInfo.ProfessionalProfile!.Affiliation,
						FullName = personInfo.FirstName + " " + personInfo.LastName,
				};

				await _editorRepository.AddAsync(editor);

				return editor;
		}
}
