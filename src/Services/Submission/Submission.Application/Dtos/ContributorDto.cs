using Blocks.Security;
using Submission.Domain.Enums;

namespace Submission.Application.Dtos;

public record ContributorDto(UserRoleType Role, PersonDto Person, HashSet<ContributionArea> ContributionAreas);
