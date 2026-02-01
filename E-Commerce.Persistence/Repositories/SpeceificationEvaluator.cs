using E_commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public static class SpeceificationEvaluator
    {
        public static IQueryable<TEntity> ApplySpeceification<TEntity>(this IQueryable<TEntity> query
                                                                      ,ISpecification<TEntity> specifications) where TEntity : class
        {
            IQueryable<TEntity> Query = query;

            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            query = specifications.Includes.Aggregate(query, (query, specification) => query.Include(specification));

            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if(specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if(specifications.IsPagingEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            return query;
        }
    }
}
