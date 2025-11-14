using E_commerce.Domain.Contracts;
using E_commerce.Domain.Entites;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class Repository<TEntity, TKey>(StoreDbContext context) : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        public void Add(TEntity entity)
            => context.Add(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync()
            =>await context.Set<TEntity>().ToListAsync();


        public async Task<TEntity>? GetByIdAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);
        

        public void Remove(TEntity entity)
            => context.Remove(entity);
        

        public void Update(TEntity entity)
            => context.Update(entity);
        
    }
}
