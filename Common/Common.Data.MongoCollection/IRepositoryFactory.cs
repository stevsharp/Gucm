using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection
{

    public interface IRepositoryFactory
    {
        IRepository<TEntity> Create<TEntity>(RepositoryOptions options);
    }
}
