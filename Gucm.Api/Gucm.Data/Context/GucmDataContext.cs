
using Gucm.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public IEnumerable<ChangeLog> GetChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(x => x.Entity.GetType().Name != "ChangeLog"))
            {
                if (entry.State != EntityState.Modified)
                    continue;

                var entityType = entry.Entity.GetType();

                foreach (var property in entityType.GetTypeInfo().DeclaredProperties)
                {
                    var originalValue = entry.Property(property.Name).OriginalValue;
                    var currentValue = entry.Property(property.Name).CurrentValue;

                    if (string.Concat(originalValue) == string.Concat(currentValue))
                        continue;
                    // todo: improve the way to retrieve primary key value from entity instance
                    var key = entry.Entity.GetType().GetProperties().First().GetValue(entry.Entity, null).ToString();

                    yield return new ChangeLog
                    {
                        ClassName = entityType.Name,
                        PropertyName = property.Name,
                        Key = key,
                        OriginalValue = originalValue == null ? string.Empty : originalValue.ToString(),
                        CurrentValue = currentValue == null ? string.Empty : currentValue.ToString(),
                        UserName = string.Empty,
                        ChangeDate = DateTime.Now
                    };
                }
            }
        }
    }
}
