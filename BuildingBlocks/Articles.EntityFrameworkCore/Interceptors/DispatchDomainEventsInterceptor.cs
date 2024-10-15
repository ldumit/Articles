using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Articles.EntityFrameworkCore;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
		private readonly TransactionOptions _transactionOptions;
		private readonly IMediator _mediator;
		private readonly TransactionProvider _transactionProvider;
		private DbTransaction? _transaction;

		public DispatchDomainEventsInterceptor(TransactionProvider transactionProvider, IOptions<TransactionOptions> transactionOptions, IMediator mediator)
				=> (_transactionProvider, _transactionOptions, _mediator) = (transactionProvider, transactionOptions.Value, mediator);

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
				if (_transactionOptions.UseSingleTransaction
						&& eventData.Context is not null 
						&& eventData.Context.Database.CurrentTransaction is null
						&& _transaction is null)
				{
						_transaction =  await _transactionProvider.BeginTransactionAsync(cancellationToken);
						await eventData.Context.Database.UseTransactionAsync(_transaction, cancellationToken);
				}

				return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
		{
				result = await base.SavedChangesAsync(eventData, result, cancellationToken);

				if(eventData.Context is not null)
						//await eventData.Context.DispatchDomainEventsAsync(cancellationToken);
						await _mediator.DispatchDomainEventsAsync(eventData.Context);

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
