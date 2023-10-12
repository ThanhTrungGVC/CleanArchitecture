using Microsoft.EntityFrameworkCore;
using Zata.Entities;

namespace Zata.Repository.MySql.EfCore.Repositories
{
    public class EfCoreRepository<TContext, TEntity> : ZataEntityFrameworkCore<TContext>, IEfCoreRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        public EfCoreRepository(TContext dbContext) : base(dbContext)
        {
        }

        protected override async ValueTask DoDisposeAsync()
        {
            var dbContext = GetDbContext();

            await dbContext.Database.CloseConnectionAsync();

            await dbContext.DisposeAsync();

        }

        public async Task<TEntity[]> GetAsync(CancellationToken cancellationToken = default)
        {
            var entities = await GetDbContext().Set<TEntity>().AsNoTracking().AsQueryable().ToArrayAsync(cancellationToken);

            return entities;
        }
    }
}
