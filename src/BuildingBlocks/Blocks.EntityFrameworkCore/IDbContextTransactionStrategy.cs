namespace Blocks.EntityFrameworkCore;

public interface IDbContextTransactionStrategy
{
		Task ExecuteAsync(Func<Task> action);
}
