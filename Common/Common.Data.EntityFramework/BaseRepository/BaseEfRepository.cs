using Common.Domain.Models;
using Common.Domain.Specification;
using Common.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gucm.Data.EntityFramework
{
    public abstract class BaseEfRepository<T> : IRepository<T> where T : EntityBase
    {
        protected BaseDbContext _context;

        protected DbSet<T> DbSet;

        protected BaseEfRepository(BaseDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> FindAllAsIQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T FindBy(object id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> FindByAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            IQueryable<T> query = DbSet;

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ExpressionSpecification<T> specification)
        {
            CheckExpression(specification);

            return await DbSet.Where(specification.ToExpression()).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncWithInclude(ExpressionSpecification<T> specification)
        {
            CheckExpression(specification);

            return await ApplySpecification(specification).ToListAsync();
        }

        private void CheckExpression(ExpressionSpecification<T> specification)
        {
            if (specification is null)
                throw new ArgumentNullException(nameof(specification));
        }

        public async Task<bool> ExistsAsync(ExpressionSpecification<T> specification)
        {
            CheckExpression(specification);

            return await DbSet.AnyAsync(specification.ToExpression());
        }

        private IQueryable<T> ApplySpecification(ExpressionSpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(DbSet.AsQueryable(), spec);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }
        /// <summary>
        /// Is used for disconnected object
        /// https://www.learnentityframeworkcore.com/dbcontext/modifying-data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfProps"></param>
        public virtual void Update(T entity, params string[] listOfProps)
        {
            if (!listOfProps.Any())
                return;

            DbSet.Attach(entity);

            foreach (var prop in listOfProps)
                _context.Entry(entity).Property(prop).IsModified = true;
        }
        /// <summary>
        ///  Is used for disconnected object
        ///  How to use
        ///   repository.Update(entry,x=>x.MarketId);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfProps"></param>
        public virtual void Update(T entity, params Expression<Func<T, object>>[] listOfProps)
        {
            if (!listOfProps.Any())
                return;

            DbSet.Attach(entity);

            foreach (var prop in listOfProps)
                _context.Entry(entity).Property(prop).IsModified = true;

        }
    }
}
