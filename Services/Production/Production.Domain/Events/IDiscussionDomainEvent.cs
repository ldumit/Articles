using Production.Domain.Enums;

namespace Production.Domain.Events;

public interface IDiscussionDomainEvent
{
    public int ArticleId { get; set; }
    public ArticleStage PreviousStage { get; set; }
    public ActionType NewAction { get; set; }
    public AssetType? AssetType { get; set; }
    public int? UserId { get; set; }
    public string Comment { get; set; }
    public DiscussionType DiscussionType { get; set; }
}
