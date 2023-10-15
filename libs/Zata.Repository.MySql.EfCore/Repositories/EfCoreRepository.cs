using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zata.Attributes;
using Zata.Auditing;
using Zata.Entities;
using Zata.Extension;
using Zata.Values;

namespace Zata.Repository.MySql.EfCore.Repositories
{
    public class EfCoreRepository<TContext, TEntity> : ZataEntityFrameworkCore<TContext>, IEfCoreRepository<TContext, TEntity> where TContext : DbContext where TEntity : Entity
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _entity;

        public EfCoreRepository(TContext dbContext) : base(dbContext)
        {
            _dbContext = GetDbContext();
            _entity = _dbContext.Set<TEntity>();
        }

        protected override async ValueTask DoDisposeAsync()
        {
            if (_dbContext.Database != null)
            {
                var connection = _dbContext.Database.GetDbConnection();

                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                    await _dbContext.Database.CloseConnectionAsync().ConfigureAwait(false);

                await _dbContext.DisposeAsync().ConfigureAwait(false);
            }
        }

        protected IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false)
        {
            var query = _entity.AsQueryable();

            if (!isTracking)
                query = query.AsNoTracking();

            if (fredicate != null)
                query = query.Where(fredicate);

            return query;
        }

        public async Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetQuery(fredicate, isTracking);

            var entities = await query.ToArrayAsync(cancellationToken).ConfigureAwait(false);

            return entities;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetQuery(fredicate, isTracking);

            TEntity? entity = await query.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            return entity;
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetQuery(fredicate, isTracking);

            TEntity entity = await query.FirstAsync(cancellationToken).ConfigureAwait(false);

            return entity;
        }

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetQuery(fredicate, isTracking);

            TEntity? entity = await query.SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            return entity;
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>>? fredicate, bool isTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetQuery(fredicate, isTracking);

            TEntity entity = await query.SingleAsync(cancellationToken).ConfigureAwait(false);

            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Check.NotNull(entity, nameof(entity));

            CheckAndSetId(entity);
            CheckAndSetCreatedDate(entity);

            var savedEntity = (await _dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false)).Entity;

            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return savedEntity;
        }

        protected virtual void CheckAndSetId(TEntity entity)
        {
            if (entity is IEntity<Guid> entity2)
            {
                TrySetGuidId(entity2);
            }
        }
        protected virtual void CheckAndSetCreatedDate(TEntity entity)
        {
            if (entity is IHasCreationTime entity2)
            {
                ObjectHelper.TrySetProperty(
                entity2, x => x.CreationTime, () => DateTime.Now, Array.Empty<Type>());
            }
        }

        protected virtual void TrySetGuidId(IEntity<Guid> entity)
        {
            if (entity.Id != default)
                return;

            EntityHelper.TrySetId(entity, () => Guid.NewGuid(), true);
        }

        public async Task<int> InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities.IsNulOrEmpty())
                return 0;

            foreach (var entity in entities)
            {
                CheckAndSetId(entity);
                CheckAndSetCreatedDate(entity);
            }

            await _dbContext.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

            var result = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            CheckAndSetModifiledDate(entity);

            var updated = _dbContext.Update(entity).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return updated;
        }

        protected void CheckAndSetModifiledDate(TEntity entity)
        {
            if (entity is IHasModificationTime entity2)
            {
                entity2.LastModificationTime = DateTime.Now;
            }
        }

        public async Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities.IsNulOrEmpty())
                return 0;

            foreach (var entity in entities)
            {
                CheckAndSetModifiledDate(entity);
            }

            _dbContext.UpdateRange(entities);
            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var deleted = _dbContext.Remove(entity).Entity;
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return deleted;
        }

        public async Task<int> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities.IsNulOrEmpty())
                return 0;

            _dbContext.RemoveRange(entities);

            return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
