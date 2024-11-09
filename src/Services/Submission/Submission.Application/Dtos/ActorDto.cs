using Articles.Security;
using Submission.Domain.Enums;

namespace Submission.Application.Dtos;

public record ActorDto(UserRoleType Role, PersonDto Person, HashSet<ContributionArea> ContributionAreas);
