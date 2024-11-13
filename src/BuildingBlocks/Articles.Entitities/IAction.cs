namespace Blocks.Domain;

public interface IAction
{
		int CreatedById { get; set; }
		DateTime CreatedOn { get; }
		public string Action { get; }
		string? Comment { get; }
}
