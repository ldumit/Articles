using Articles.Entitities;
using Submission.Domain.ValueObjects;

namespace Submission.Domain.Entities;

public partial class File : IDomainObject, IValueObject
{
    //talk - difference between required, null!, default!
    public string OriginalName { get; init; } = default!;    
    //talk - server id, create the file server server both mongo & azure blob
    public string FileServerId { get; init; } = default!;
    public long Size { get; set; }
    public required FileName Name { get; init; }
		public FileExtension Extension { get; init; } = default!;
}