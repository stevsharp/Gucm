using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Gucm.Data.Context;
using Common.Infrastructure.UnitOfWork;
using System.Linq;

namespace Gucm.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GucmDataContext _context;
        public UnitOfWork(GucmDataContext context) => _context = context;

        public Task<IDbContextTransaction> BeginTransaction() => _context.Database.BeginTransactionAsync();

        public IExecutionStrategy CreateExecutionStrategy() => _context.Database.CreateExecutionStrategy();

        public int SaveChanges() => _context.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changes = _context.GetChanges().ToList();

            if (changes.Any())
                _context.Set<ChangeLog>().AddRange(changes);

            return _context.SaveChangesAsync(cancellationToken);
        }

        public void UseExtenalUseTransaction(System.Data.Common.DbTransaction transaction) => _context.Database.UseTransaction(transaction);
    }
}
