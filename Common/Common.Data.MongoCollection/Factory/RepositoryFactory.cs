using System;

namespace Common.Data.MongoCollection
{

    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IDbFactory _dbFactory;

        public RepositoryFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        }

        public IRepository<TEntity> Create<TEntity>(RepositoryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var db = _dbFactory.GetDatabase(options.ConnectionString, options.DatabaseName);
            return new Repository<TEntity>(db.GetCollection<TEntity>(options.CollectionName));
        }
    }

}
