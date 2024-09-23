using Articles.Abstractions;
using Articles.Entitities;
using Articles.System.Cache;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class TimelineTemplate : Entity, ICacheable
{
    public SourceType SourceType { get; set; }
    public int SourceId { get; set; }
    public ArticleStage ArticleStage { get; set; }
    public string TitleTemplate { get; set; } = default!;
    public string DescriptionTemplate { get; set; } = default!;
}
