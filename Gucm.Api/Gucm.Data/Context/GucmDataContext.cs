
using Gucm.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gucm.Data.Context
{

    public partial class GucmDataContext : BaseDbContext
    {
        public GucmDataContext(DbContextOptions<GucmDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder, Constant.Yield, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

        }
    }
}
