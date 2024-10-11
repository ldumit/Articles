using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;

namespace Articles.EntityFrameworkCore;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
		private readonly TransactionOptions _transactionOptions;
		private readonly DbConnection _dbConection;
		private DbTransaction? _transaction;

		public DispatchDomainEventsInterceptor(DbConnection dbConection,  IOptions<TransactionOptions> transactionOptions)
				=> (_dbConection, _transactionOptions) = (dbConection, transactionOptions.Value);

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
				if (eventData.Context != null 
						&& eventData.Context.Database.CurrentTransaction == null 
						&& _transactionOptions.UseSingleTransaction
						&& _transaction == null)
				{
						if(_dbConection.State == ConnectionState.Closed)
								await _dbConection.OpenAsync(cancellationToken);
						_transaction = await _dbConection.BeginTransactionAsync(cancellationToken);
						await eventData.Context.Database.UseTransactionAsync(_transaction);
				}

				return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
				result = await base.SavedChangesAsync(eventData, result, cancellationToken);

				if(eventData.Context is not null)
						await eventData.Context.DispatchDomainEventsAsync(cancellationToken);

				if (_transaction != null)
						await _transaction.CommitAsync(cancellationToken);

				return result;
		}

		public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
		{
				if (_transaction != null)
						await _transaction.RollbackAsync(cancellationToken);

				await base.SaveChangesFailedAsync(eventData, cancellationToken);
		}
}
