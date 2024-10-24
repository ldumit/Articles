using Articles.Entitities;

namespace Submission.Domain.Entities;

public class FileLatestAction : IChildEntity
{
    public int FileId { get; set; }
    public File File  { get; set; } = null!;
    public int ActionId { get; set; }
    public FileAction Action { get; set; } = null!;
}