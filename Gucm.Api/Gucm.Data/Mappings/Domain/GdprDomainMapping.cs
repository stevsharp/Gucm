using Gucm.Data.EntityFramework;
using Gucm.Domain.Gdpr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gucm.Data.Mappings.Domain
{
    [DBDataContext(Constant.Yield)]
    public class GdprDomainMapping : IEntityTypeConfiguration<GdprDomain>
    {
        public void Configure(EntityTypeBuilder<GdprDomain> builder)
        {
            builder.ToTable("GdprTable", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            builder.Property(p=>p.Id).UseSqlServerIdentityColumn();

            builder.Property(p => p.Gdpr);
        }
    }
}
