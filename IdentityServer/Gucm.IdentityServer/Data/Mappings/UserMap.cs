using Gucm.IdentityServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gucm.IdentityServer.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.ConcurrencyStamp).HasMaxLength(36);
            builder.Property(e => e.FirstName).HasMaxLength(35);
            builder.Property(e => e.LastName).HasMaxLength(35);
            builder.Property(e => e.PasswordHash).HasMaxLength(84);
            builder.Property(e => e.PhoneNumber).HasMaxLength(15);
            builder.Property(e => e.SecurityStamp).HasMaxLength(36);
            builder.Property(e => e.Password).HasMaxLength(128);
            builder.Property(e => e.JobTitle).HasMaxLength(64);
            builder.Property(e => e.EmailConfirmationToken).HasMaxLength(254);
            builder.Property(e => e.ResetPasswordToken).HasMaxLength(254);

            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
