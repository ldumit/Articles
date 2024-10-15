using Articles.Abstractions;
using Articles.Entitities;
using ArticleTimeline.Domain.Enums;

namespace ArticleTimeline.Domain;

public class Timeline : AggregateEntity
{
    public int ArticleId { get; init; }
    public ArticleStage NewStage { get; init; }
		public ArticleStage CurrentStage { get; init; }
		public SourceType SourceType { get; init; }
    public string SourceId { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
		public TimelineTemplate Template { get; private set; } = null!;
}
