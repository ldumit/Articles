using Articles.Security;

namespace Submission.Application.Dtos;

public record ContributorDto(UserRoleType Role, PersonDto Person, HashSet<ContributionArea> ContributionAreas);
