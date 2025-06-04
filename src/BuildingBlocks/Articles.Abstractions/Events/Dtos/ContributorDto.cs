using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record ContributorDto(
		UserRoleType Role, 
		PersonDto Person
		); 
