namespace Catalog.Domain.Repositories
{
    public interface IRepository
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}