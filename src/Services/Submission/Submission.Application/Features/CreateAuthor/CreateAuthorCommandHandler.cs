using Auth.Grpc;
using Blocks.Exceptions;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Submission.Application.Features.CreateAuthor;

public class CreateAuthorCommandHandler(Repository<Person> _personRepository, IPersonService _personClient)
		: IRequestHandler<CreateAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateAuthorCommand command, CancellationToken ct)
		{
				if (await _personRepository.Entity.AnyAsync(p => p.Email.Value.ToLower() == command.Email.ToLower(), ct))
						throw new BadRequestException("Author with this email already exists.");

				var personInfo = await CreatePerson(command, ct);

				var author = Author.Create(personInfo, command);

				await _personRepository.AddAsync(author);

				await _personRepository.SaveChangesAsync();

				return new IdResponse(author.Id);
		}

		private async Task<PersonInfo> CreatePerson(CreateAuthorCommand command, CancellationToken ct)
		{
				var createPersonRequest = command.Adapt<CreatePersonRequest>();
				var response = await _personClient.CreatePersonAsync(createPersonRequest, new CallOptions(cancellationToken: ct));
				return response.PersonInfo;
		}
}
