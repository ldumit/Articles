using Blocks.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Blocks.EntityFrameworkCore;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
		private readonly TransactionOptions _transactionOptions;
		private readonly IDomainEventPublisher _eventPublisher;
		private readonly TransactionProvider _transactionProvider;
		private DbTransaction? _transaction;

		public DispatchDomainEventsInterceptor(TransactionProvider transactionProvider, IOptions<TransactionOptions> transactionOptions, IDomainEventPublisher eventPublisher)
				=> (_transactionProvider, _transactionOptions, _eventPublisher) = (transactionProvider, transactionOptions.Value, eventPublisher);

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
		{
				if (_transactionOptions.UseSingleTransaction
						&& eventData.Context is not null 
						&& eventData.Context.Database.CurrentTransaction is null
						&& _transaction is null)
				{
						_transaction =  await _transactionProvider.BeginTransactionAsync(ct);
						await eventData.Context.Database.UseTransactionAsync(_transaction, ct);
				}

				return await base.SavingChangesAsync(eventData, result, ct);
		}

		public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken ct = default)
		{
				result = await base.SavedChangesAsync(eventData, result, ct);

				if(eventData.Context is not null)
						await eventData.Context.DispatchDomainEventsAsync(_eventPublisher, ct);

				if (_transaction != null)
						await _transaction.CommitAsync(ct);

				return result;
		}

		public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
		{
				if (_transaction != null)
						await _transaction.RollbackAsync(cancellationToken);

				await base.SaveChangesFailedAsync(eventData, cancellationToken);
		}
}
