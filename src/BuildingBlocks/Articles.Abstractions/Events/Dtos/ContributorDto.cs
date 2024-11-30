using Articles.Security;

namespace Articles.Abstractions.Events.Dtos;

public record ContributorDto(
		UserRoleType Role, 
		PersonDto Person
		); 
