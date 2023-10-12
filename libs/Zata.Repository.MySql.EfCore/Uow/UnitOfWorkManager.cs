namespace Zata.Repository.MySql.EfCore.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IUnitOfWork> BeginTransactionAsync(bool isRequireNew = false, CancellationToken cancellationToken = default)
        {
            await _uow.BeginTransactionAsync(isRequireNew, cancellationToken);
            return _uow;
        }
    }
}
