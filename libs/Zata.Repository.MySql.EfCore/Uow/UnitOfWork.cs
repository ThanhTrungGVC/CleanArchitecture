using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public class UnitOfWork<TContext> : ZataEntityFrameworkCore<TContext>, IUnitOfWork<TContext> where TContext : DbContext
    {
        private DbTransaction _dbTransaction = default!;

        public UnitOfWork(TContext dbContext) : base(dbContext)
        {
        }

        protected override async ValueTask DoDisposeAsync()
        {
            await _dbTransaction.DisposeAsync();
        }

        public async Task<DbTransaction> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default)
        {
            var db = await GetOpenConnectionAsync(cancellationToken).ConfigureAwait(false);

            DbTransaction? currentTransaction = default;

            if (!isRequireNew)
                currentTransaction = GetCurrentTransaction();

            _dbTransaction = currentTransaction ?? await db.BeginTransactionAsync(cancellationToken);

            return _dbTransaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default) => await _dbTransaction.CommitAsync(cancellationToken);

        public async Task RollbackAsync(CancellationToken cancellationToken = default) => await _dbTransaction.RollbackAsync(cancellationToken);
    }
}
