using Articles.Security;

namespace Review.Application.Dtos;

public record ContributorDto(UserRoleType Role, PersonDto Person, HashSet<ContributionArea> ContributionAreas);
