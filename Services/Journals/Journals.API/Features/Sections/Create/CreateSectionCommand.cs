using FastEndpoints;

namespace Journals.API.Features.Sections.Create;

public record CreateSectionCommand(int JournalId, string Name, string Description);

public class CreateSectionCommandValidator : Validator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {

    }
}