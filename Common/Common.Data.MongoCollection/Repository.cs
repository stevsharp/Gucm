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

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            var cursor = await _collection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> filter, UpdateDefinition<TEntity> update)
        {
            return _collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<TEntity, TEntity>() { ReturnDocument = ReturnDocument.After });
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var cursor = await _collection.FindAsync(x => true);

            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(PagingOptions pagingOptions)
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

        public Task InsertOneAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task<TEntity> UpsertOneAsync(Expression<Func<TEntity, bool>> filter, TEntity entity)
        {
            return _collection.FindOneAndReplaceAsync(filter, entity,
                                                      new FindOneAndReplaceOptions<TEntity, TEntity>()
                                                      {
                                                          IsUpsert = true
                                                      });
        }
    }

}
