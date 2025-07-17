using Journals.Domain.Journals.Enums;

namespace Journals.API.Features.Shared;

public record EditorDto(
		string FullName, 
		string Location, 
		string University, 
		EditorRole Role);