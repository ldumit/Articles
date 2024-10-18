using FastEndpoints;

namespace Journals.API.Features.Journals.Update;

public record UpdateJournalCommand(int Id, string Abbreviation, string ShortName, string Name, string Description, string ISSN, int ChiefEditorId);

public class UpdateJournalCommandValidator : Validator<UpdateJournalCommand>
{
    public UpdateJournalCommandValidator()
    {

    }
}