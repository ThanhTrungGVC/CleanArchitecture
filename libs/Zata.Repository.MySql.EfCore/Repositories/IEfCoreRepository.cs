using Microsoft.EntityFrameworkCore;
using Zata.Entities;

namespace Zata.Repository.MySql.EfCore.Repositories
{
    public interface IEfCoreRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        Task<TEntity[]> GetAsync(CancellationToken cancellationToken = default);
    }
}
