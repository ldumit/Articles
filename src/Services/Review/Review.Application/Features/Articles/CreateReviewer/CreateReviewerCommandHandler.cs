using Microsoft.EntityFrameworkCore;
using Blocks.Exceptions;

namespace Review.Application.Features.Articles.CreateReviewer;

public class CreateReviewerCommandHandler(Repository<Person> _personRepository)
        : IRequestHandler<CreateReviewerCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateReviewerCommand command, CancellationToken cancellationToken)
    {
        var exists = await _personRepository.Entity.AnyAsync(p => p.Email.Value.EqualsIgnoreCase(command.Email) && nameof(Reviewer) == p.TypeDiscriminator);
				if (exists)
						throw new BadRequestException("A reviewer with this email already exists.");

        var reviewer = Reviewer.Create(command.Email, command.FirstName, command.LastName, command.Honorific.ToString(), command.Affiliation, command);
        await _personRepository.AddAsync(reviewer);

        await _personRepository.SaveChangesAsync();

        return new IdResponse(reviewer.Id);
    }
}
