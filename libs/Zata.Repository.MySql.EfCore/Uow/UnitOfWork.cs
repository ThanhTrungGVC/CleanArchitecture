using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public class UnitOfWork<TContext> : ZataEntityFrameworkCore<TContext>, IUnitOfWork where TContext : DbContext
    {
        private IDbContextTransaction _dbTransaction = default!;

        public UnitOfWork(TContext dbContext) : base(dbContext)
        {
        }

        protected override async ValueTask DoDisposeAsync()
        {
            await _dbTransaction.DisposeAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default)
        {
            var db = GetDbContext().Database;

            IDbContextTransaction? currentTransaction = default;

            if (!isRequireNew)
                currentTransaction = db.CurrentTransaction;

            _dbTransaction = currentTransaction ?? await db.BeginTransactionAsync(cancellationToken);

            return _dbTransaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default) => await _dbTransaction.CommitAsync(cancellationToken);

        public async Task RollbackAsync(CancellationToken cancellationToken = default) => await _dbTransaction.RollbackAsync(cancellationToken);

        public async Task CreateSavepointAsync(string name, CancellationToken cancellationToken = default) => await _dbTransaction.CreateSavepointAsync(name, cancellationToken);
        public async Task RollbackToSavepointAsync(string name, CancellationToken cancellationToken = default) => await _dbTransaction.RollbackToSavepointAsync(name, cancellationToken);
        public async Task ReleaseSavepointAsync(string name, CancellationToken cancellationToken = default) => await _dbTransaction.ReleaseSavepointAsync(name, cancellationToken);
    }
}
