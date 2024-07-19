using Articles.Entitities;

namespace Production.Domain.Entities;

public class FileLatestAction : ChildEntity
{
    public int FileId { get; set; }
    public File File  { get; set; } = null!;
    public int ActionId { get; set; }
    public FileAction Action { get; set; } = null!;
}