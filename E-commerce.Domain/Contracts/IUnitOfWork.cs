using E_commerce.Domain.Entites;

namespace E_commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, Tkey> GetRepository<TEntity,Tkey> () where TEntity : Entity<Tkey>;
        Task<int> SaveChangesAsync();
    }
}
