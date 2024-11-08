using Articles.Security;

namespace Production.Application.Dtos;

public record ActorDto(UserRoleType Role, PersonDto Person);
