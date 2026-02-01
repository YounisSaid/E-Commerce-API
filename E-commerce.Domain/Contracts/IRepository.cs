using E_commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Contracts
{
    public interface IRepository <TEntity,TKey> where TEntity : Entity<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<int> CountAsync(ISpecification<TEntity> specification);
        Task<TEntity>? GetByIdAsync(TKey id);
        Task<TEntity>? GetAsync(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
    }
}
