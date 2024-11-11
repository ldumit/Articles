using Articles.Abstractions.Enums;
using Articles.Entitities;

namespace Submission.Domain.Entities;

public partial class StageHistory : Entity
{
    public required DateTime StartDate { get; init; }

    public required ArticleStage StageId { get; init; }
    //public Stage Stage { get; init; } = null!;

    public required int ArticleId { get; init; }
    //public virtual Article Article { get; set; } = null!;
}
