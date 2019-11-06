using Gucm.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gucm.Data.Mappings
{
    [DBDataContext(Constant.OData)]
    public class GdprTableMapping : IEntityTypeConfiguration<GdprTable>
    {
        public void Configure(EntityTypeBuilder<GdprTable> builder)
        {
            builder.ToTable("GdprTable", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Gdpr);
        }
    }
}
