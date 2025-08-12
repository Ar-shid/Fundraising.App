using FundRaising.Common;
using FundRaising.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FundRaising.Data.Configuration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(e => new { e.RoleId, e.PermissionId });

            builder.Property(e => e.RoleId)
                .HasMaxLength(ModelConstants.RoleIdMaxLength)
                .IsRequired();

            builder.HasOne(e => e.Permission)
                .WithMany(p => p.Roles)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
