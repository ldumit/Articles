using FastEndpoints;
using Journals.API.Features.Shared;

namespace Journals.API.Features.Journals.Get;

public record GetJournalQuery(int JournalId);
public record GetJournalResponse(JournalDto Journal);



public class GetJournalQueryValidator : Validator<GetJournalQuery>
{
    public GetJournalQueryValidator()
    {

    }
}