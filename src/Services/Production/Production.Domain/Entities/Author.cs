namespace Production.Domain.Entities;

public partial class Author : Person
{
		//todo - this should be part of Contributor because it might change for different articles?!?
		public required string Affiliation { get; init; }
}
