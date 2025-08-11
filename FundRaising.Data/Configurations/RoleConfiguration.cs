using FundRaising.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundRaising.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .HasMany(e => e.Users)
               .WithOne(r => r.Role)
               .HasForeignKey(e => e.RoleId)
               .IsRequired();
        }
    }
}
