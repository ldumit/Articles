namespace Articles.Abstractions.Events.Dtos;

public record PersonDto(
		int Id, 
		string FirstName, 
		string LastName, 
		string Email, 
		string? Title,
		string? Affiliation,
		int? UserId
		);