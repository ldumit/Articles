using FastEndpoints;

namespace Journals.API.Features.Sections.Update;

public record UpdateSectionCommand(int JournalId, int Id, string Name, string Description);

public class UpdateSectionCommandValidator : Validator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {

    }
}