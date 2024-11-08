using Articles.Entitities;

namespace Ordering.Domain.Models;

public abstract record ValueObject : IDomainObject;

public record Address : ValueObject
{
    public required string Value { get; set; }
    public required string City { get; set; }
    public string PostCode { get; set; } = default!;
}
