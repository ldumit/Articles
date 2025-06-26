using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record ActorDto(
		UserRoleType Role, 
		PersonDto Person
		); 
