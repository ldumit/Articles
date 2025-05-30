﻿using Articles.Abstractions.Enums;
using Blocks.Entitities;

namespace Production.Domain.Entities;

public partial class StageHistory : Entity
{
    public DateTime StartDate { get; set; }

    public ArticleStage StageId { get; set; }
    public Stage Stage { get; set; } = null!;

    public int ArticleId { get; set; }
    public virtual Article Article { get; set; } = null!;
}
