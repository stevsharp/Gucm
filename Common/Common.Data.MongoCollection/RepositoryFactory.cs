using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection
{

    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IDbFactory _dbFactory;

        public RepositoryFactory(IDbFactory dbFactory)
        {
            if (dbFactory == null)
                throw new ArgumentNullException(nameof(dbFactory));
            _dbFactory = dbFactory;
        }

        public IRepository<TEntity> Create<TEntity>(RepositoryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var db = _dbFactory.GetDatabase(options.ConnectionString);
            return new Repository<TEntity>(db.GetCollection<TEntity>(options.CollectionName));
        }
    }

}
