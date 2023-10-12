using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Zata.Repository.MySql.EfCore
{
    public abstract class ZataEntityFrameworkCore<TContext> : IAsyncDisposable where TContext : DbContext
    {
        private readonly TContext _dbContext;

        protected ZataEntityFrameworkCore(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected TContext GetDbContext() => _dbContext;

        protected DbConnection GetDbConnection() => _dbContext.Database.GetDbConnection();

        protected DbTransaction? GetDbTransaction() => _dbContext.Database.CurrentTransaction?.GetDbTransaction();

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            GC.SuppressFinalize(this);
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                await DoDisposeAsync();
            }

            this.disposed = true;
        }

        protected abstract ValueTask DoDisposeAsync();

        protected bool disposed = false;
    }
}
