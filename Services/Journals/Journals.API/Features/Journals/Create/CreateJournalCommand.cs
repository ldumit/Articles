using FastEndpoints;

namespace Journals.API.Features.Journals.Create;

public record CreateJournalCommand(string Abbreviation, string ShortName, string Name, string Description, string ISSN, int ChiefEditorId);

public class CreateJournalCommandValidator : Validator<CreateJournalCommand>
{
    public CreateJournalCommandValidator()
    {

    }
}