namespace Articles.EntityFrameworkCore;

public interface IDbContextTransactionStrategy
{
		Task ExecuteAsync(Func<Task> action);
}
