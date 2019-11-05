using Common.Domain.Models;
using Common.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Infrastructure.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(ExpressionSpecification<T> specification);
        Task<IReadOnlyList<T>> GetAllAsyncWithInclude(ExpressionSpecification<T> specification);
        T FindBy(object id);
        Task<T> FindByAsync(object id);
        Task<bool> ExistsAsync(ExpressionSpecification<T> specification);
        void Update(T entity);
        void Update(T entity, params string[] listOfProps);
        void Update(T entity, params Expression<Func<T, object>>[] listOfProps);
        void Add(T entity);
        void Remove(T entity);


    }
}
