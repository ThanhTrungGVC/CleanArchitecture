using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public interface IUnitOfWork<TContext> : IAsyncDisposable where TContext : DbContext
    {
        /// <summary>
        /// Started one database transaction
        /// </summary>
        /// <param name="isRequireNew"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DbTransaction> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Commits all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information and examples.
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
