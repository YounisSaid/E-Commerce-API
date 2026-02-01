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
        private readonly DbSet<TEntity> _dbset = context.Set<TEntity>();
        public void Add(TEntity entity)
            => context.Add(entity);

        public async Task<int> CountAsync(ISpecification<TEntity> specification)
            => await _dbset.ApplySpeceification(specification).CountAsync();
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            =>await _dbset.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
          => await _dbset.ApplySpeceification(specification).ToListAsync();

        public async Task<TEntity>? GetAsync(ISpecification<TEntity> specification)
          => await _dbset.ApplySpeceification(specification).FirstOrDefaultAsync();


        public async Task<TEntity>? GetByIdAsync(TKey id)
            => await _dbset.FindAsync(id);
        

        public void Remove(TEntity entity)
            => _dbset.Remove(entity);
        

        public void Update(TEntity entity)
            => _dbset.Update(entity);
        
    }
}
