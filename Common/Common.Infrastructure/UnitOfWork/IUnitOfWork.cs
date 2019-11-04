using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IExecutionStrategy CreateExecutionStrategy();
        Task<IDbContextTransaction> BeginTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// The method was created to brigde ADO.NET and and an EF context
        /// </summary>
        /// <param name="transaction"></param>
        void UseExtenalUseTransaction(System.Data.Common.DbTransaction transaction);
    }
}
