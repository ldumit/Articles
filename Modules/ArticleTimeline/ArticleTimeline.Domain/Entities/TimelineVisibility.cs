using Articles.Abstractions;
using Articles.Entitities;
using Articles.Security;
using Articles.System.Cache;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class TimelineVisibility : Entity, ICacheable
{
    public SourceType SourceType { get; set; }
    public int SourceId { get; set; } // todo - this should hold ActionType for both Article & Asset
    public UserRoleType RoleType{ get; set; }
    public ArticleStage Stage { get; set; }
}
