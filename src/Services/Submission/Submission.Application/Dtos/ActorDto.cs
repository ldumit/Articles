using Articles.Security;

namespace Submission.Application.Dtos;

public record ActorDto(UserRoleType Role, PersonDto Person);
