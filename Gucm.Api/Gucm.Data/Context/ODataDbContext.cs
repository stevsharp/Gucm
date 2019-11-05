
using Gucm.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gucm.Data.Context
{
    public class ODataDbContext : BaseDbContext
    {

        public ODataDbContext(DbContextOptions<ODataDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder, Constant.OData, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

    }
}
