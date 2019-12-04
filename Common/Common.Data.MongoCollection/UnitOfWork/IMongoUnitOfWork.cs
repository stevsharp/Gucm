using System;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection.UnitOfWork
{
    public interface IMongoUnitOfWork
    {
        Task<int> SaveChanges();

        void AddCommand<TEntity>(Func<Task<TEntity>> func);
    }
}
