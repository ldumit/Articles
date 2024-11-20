using Blocks.Entitities;
using Blocks.Core.Cache;
using Articles.Security;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class TimelineVisibility : ICacheable, IMetadataEntity
{
    public SourceType SourceType { get; init; }
    public string SourceId { get; init; } = null!;
    public UserRoleType RoleType{ get; init; }
}
