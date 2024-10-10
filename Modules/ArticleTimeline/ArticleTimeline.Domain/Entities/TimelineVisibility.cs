using Articles.Entitities;
using Articles.Security;
using Articles.System.Cache;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class TimelineVisibility : ICacheable, IMetadataEntity
{
    public SourceType SourceType { get; set; }
    public string SourceId { get; set; } = null!; // todo - this should hold ActionType for both Article & Asset
    public UserRoleType RoleType{ get; set; }
}
