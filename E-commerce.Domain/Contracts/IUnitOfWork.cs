using E_commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, Tkey> GetRepository<TEntity,Tkey> () where TEntity : Entity<Tkey>;
        Task<int> SaveChangesAsync();
    }
}
