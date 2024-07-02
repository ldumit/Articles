using Articles.Entitities;
using Production.Domain.Enums;

namespace Production.Domain.Entities;

public partial class StageHistory : TenantEntity
{
    public DateTime StartDate { get; set; }

    public ArticleStagesCode StageId { get; set; }

    public int ArticleId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }
    public virtual Article? Article { get; set; }

}
