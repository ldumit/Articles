using Production.Domain.Enums;

namespace Production.Domain.Events;

public record ArticleStageChangedDomainEvent : DomainEvent, IDiscussionDomainEvent
{
    public ArticleStage NewStage { get; set; }
    public DateTime? CreateTime { get; set; }
    public string UserName { get; set; }
    public string RoleCode { get; set; }
    public DiscussionType DiscussionType { get; set; }
    public ArticleStage PreviousStage { get; set; }
    public FileActionType NewAction { get; set; }
    public AssetType? AssetType { get; set; }
}