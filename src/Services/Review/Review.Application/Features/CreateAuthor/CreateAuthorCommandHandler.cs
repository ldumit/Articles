using Microsoft.EntityFrameworkCore;
using Blocks.Exceptions;

namespace Review.Application.Features.CreateAuthor;

public class CreateAuthorCommandHandler(Repository<Person> _personRepository)
		: IRequestHandler<CreateAuthorCommand, IdResponse>
{
		public async Task<IdResponse> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
		{
				if (await _personRepository.Entity.AnyAsync(p => p.Email.Value.ToLower() == command.Email.ToLower()))
						throw new BadRequestException("Author with this email already exists.");

				var author = Author.Create(command.Email, command.FirstName, command.LastName, command.Title, command.Affiliation, command);
				await _personRepository.AddAsync(author);

				await _personRepository.SaveChangesAsync();

				return new IdResponse(author.Id);
		}
}
