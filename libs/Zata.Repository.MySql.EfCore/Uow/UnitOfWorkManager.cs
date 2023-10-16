using Microsoft.EntityFrameworkCore;

namespace Zata.Repository.MySql.EfCore.Uow
{
    public class UnitOfWorkManager<TContext> : IUnitOfWorkManager<TContext> where TContext : DbContext
    {
        private readonly IUnitOfWork<TContext> _uow;

        public UnitOfWorkManager(IUnitOfWork<TContext> uow)
        {
            _uow = uow;
        }

        public async Task<IUnitOfWork<TContext>> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default)
        {
            await _uow.BeginTransactionAsync(isRequireNew, cancellationToken);
            return _uow;
        }
    }
}
