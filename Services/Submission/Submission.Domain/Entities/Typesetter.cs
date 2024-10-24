namespace Submission.Domain.Entities;

public partial class Typesetter : Person
{
    public bool? IsDefault { get; set; }
    public string? CompanyName { get; set; }
}
