using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites;
using E_Commerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : Entity<Tkey>
        {
            var typeName = typeof(TEntity).Name;

            if(_repositories.TryGetValue(typeName, out var repository)) 
               return (repository as IRepository<TEntity, Tkey>)!;

            var newRepo = new Repository<TEntity, Tkey>(context);

            _repositories.Add(typeName, newRepo);
            return newRepo;

        }

        public Task<int> SaveChangesAsync()
            => context.SaveChangesAsync();


    }
}
