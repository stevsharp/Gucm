using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Data.MongoCollection.UnitOfWork
{

    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        public readonly IDbFactory _dbFactory;

        private readonly List<Func<Task>> _mongoCommands;

        public MongoUnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;

            _mongoCommands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            using (var session = await _dbFactory.dbClient.StartSessionAsync())
            {
                session.StartTransaction();
                var commandTasks = _mongoCommands.Select(c => c());
                await Task.WhenAll(commandTasks).ConfigureAwait(false);
                await session.CommitTransactionAsync().ConfigureAwait(false);
            }

            return _mongoCommands.Count;
        }
        /// <summary>
        /// How to use
        /// Func<Task<Contract>> myFun = async () => await _uniqueContractRepository.UpsertOneAsync(x => x.ContractID == dbContract.ContractID, contract); 
        // _mongoUnitOfWork.AddCommand(myFun);
        // await _mongoUnitOfWork.SaveChanges();
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="func"></param>
        public void AddCommand<TEntity>(Func<Task<TEntity>> func)
        {
            _mongoCommands.Add(func);
        }
    }
}
