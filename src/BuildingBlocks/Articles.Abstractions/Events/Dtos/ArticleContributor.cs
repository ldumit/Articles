using Articles.Security;

namespace Articles.Abstractions.Events.Dtos;

public record ArticleContributor(int Id, string Affiliation, UserRoleType Role, int ArticleId, int PersonId, PersonDto Person); 
