using Journals.Domain.Entities;

namespace Journals.API.Features.Shared;

public record EditorDto(
		string FullName, 
		string Location, 
		string University, 
		EditorRole Role);