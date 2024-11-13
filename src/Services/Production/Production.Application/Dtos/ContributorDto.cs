using Blocks.Security;

namespace Production.Application.Dtos;

public record ContributorDto(UserRoleType Role, PersonDto Person);
