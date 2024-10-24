using Articles.Entitities;
using Submission.Domain.Enums;

namespace Submission.Domain.Entities;

public partial class FileAction : AggregateEntity //talk - modification never happens for an action
{
    public int FileId { get; set; }

    //talk - difference between default! & null!
    public string Comment { get; set; } = default!;

    public AssetActionType TypeId { get; set; }

    public virtual File File { get; set; } = null!;
}
