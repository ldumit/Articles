using FastEndpoints;
using Journals.API.Features.Shared;

namespace Journals.API.Features.Sections.GetById;

public record GetSectionQuery(int JournalId, int SectionId);
public record GetSectionResponse(SectionDto Section);



public class GetJournalQueryValidator : Validator<GetSectionQuery>
{
    public GetJournalQueryValidator()
    {

    }
}