using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Zata.Entities;

namespace Zata.Repository.MySql.EfCore.Repositories
{
    public interface IEfCoreRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default);

        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
