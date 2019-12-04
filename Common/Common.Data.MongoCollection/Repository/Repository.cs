using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection
{
    public class Repository<TEntity> : IRepository<TEntity>
    {
        public readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoCollection<TEntity> collection)
        {
            _collection = collection;

            this.CollectionName = _collection.CollectionNamespace.CollectionName;
        }

        public string CollectionName { get; }

        public virtual Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter) => _collection.FindOneAndDeleteAsync(filter);

        public async virtual Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> filter) => await _collection.DeleteManyAsync(filter);

        public async virtual Task<TEntity> Find(Expression<Func<TEntity, bool>> filter) => await _collection.Find(filter).FirstOrDefaultAsync();

        public async virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public virtual Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> update) => _collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<TEntity, TEntity>() { ReturnDocument = ReturnDocument.After });

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            var cursor = await _collection.FindAsync(x => true);

            return await cursor.ToListAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll(PagingOptions pagingOptions)
        {
            var options = new FindOptions<TEntity>();
            if (null != pagingOptions)
            {
                options.Limit = pagingOptions.PageSize;
                options.Skip = pagingOptions.Offset;
            }

            var cursor = await _collection.FindAsync(x => true, options);

            return await cursor.ToListAsync();
        }

        public virtual Task InsertBatchAsync(IEnumerable<TEntity> entities) => _collection.InsertManyAsync(entities);

        public virtual Task InsertOneAsync(TEntity entity) => _collection.InsertOneAsync(entity);

        public virtual Task<TEntity> UpsertOneAsync(Expression<Func<TEntity, bool>> filter, TEntity entity) => _collection.FindOneAndReplaceAsync(filter, entity,
                                                      new FindOneAndReplaceOptions<TEntity, TEntity>()
                                                      {
                                                          IsUpsert = true
                                                      });

        public async virtual Task<IEnumerable<TEntity>> FindWithProjection(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projection)
        {
            return await _collection.Find(filter)
                                        .Project<TEntity>(projection).ToListAsync();
        }
    }

}
