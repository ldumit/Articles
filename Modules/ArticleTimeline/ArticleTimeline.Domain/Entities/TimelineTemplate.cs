using Articles.Entitities;
using Articles.System.Cache;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class TimelineTemplate : ICacheable, IMetadataEntity
{
    public SourceType SourceType { get; set; }
    public string SourceId { get; set; } = null!;
    //public ArticleStage ArticleStage { get; set; }
    public string TitleTemplate { get; set; } = default!;
    public string DescriptionTemplate { get; set; } = default!;
}
