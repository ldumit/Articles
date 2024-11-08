namespace Production.Domain.Entities;

public partial class Author : Person
{
		public string? Country { get; set; }
		public string? Biography { get; set; }
}
