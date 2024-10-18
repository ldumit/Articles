namespace Journals.API.Features.Shared;

public record JournalDto(
		int Id, 
		string Abbreviation, 
		string Name, 
		string Description, 
		string ISSN,
		List<SectionDto> Sections)
{
		//talk about the difference between the two approaches AdaptIgnore vs MapToConstructor
		//[AdaptIgnore] 
		public EditorDto ChiefEditor { get; set; } = null!;
}
