using E_commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Specifications
{
    public abstract class BaseSpecificiation<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        protected BaseSpecificiation(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }

      
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> expression)
            => Includes.Add(expression);
        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
            => OrderBy = expression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
            => OrderByDescending = expression;

        protected void AddPaging(int pageSize, int pageIndex)
        {
            IsPagingEnabled = true;
            Take = pageSize;
            Skip = pageSize * (pageIndex - 1);
        }
    }
}
