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

        protected DbConnection GetCurrentConnection() => _dbContext.Database.GetDbConnection();

        protected async Task<DbConnection> GetOpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (!await _dbContext.Database.CanConnectAsync(cancellationToken).ConfigureAwait(false))
                throw new InvalidOperationException("can't connecting to database");

            var connection = GetCurrentConnection();
            
            if (connection.State == System.Data.ConnectionState.Closed)
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            return connection;
        } 

        protected DbTransaction? GetCurrentTransaction() => _dbContext.Database.CurrentTransaction?.GetDbTransaction();

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
