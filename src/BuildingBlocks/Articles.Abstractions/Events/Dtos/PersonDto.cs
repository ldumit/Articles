namespace Articles.Abstractions.Events.Dtos;

public record PersonDto(
		int Id, 
		string FirstName, 
		string LastName, 
		string Email, 
		string? Honorific,
		string? Affiliation,
		int? UserId,
		string TypeDiscriminator
		);