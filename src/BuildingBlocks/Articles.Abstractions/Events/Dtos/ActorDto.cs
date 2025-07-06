using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public record ActorDto(
		UserRoleType Role,
		HashSet<ContributionArea> ContributionAreas,
		PersonDto Person
		);

public record AuthorDto(
		HashSet<ContributionArea> ContributionAreas,
		PersonDto Person
		);
