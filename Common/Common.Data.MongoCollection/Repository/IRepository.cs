using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindWithProjection(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projection);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(PagingOptions pagingOptions);

        Task InsertOneAsync(TEntity enity);

        Task InsertBatchAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpsertOneAsync(Expression<Func<TEntity, bool>> filter, TEntity entity);

        Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> update);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter);

        Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> filter);

        string CollectionName { get; }
    }

}
