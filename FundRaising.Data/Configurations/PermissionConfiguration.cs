using FundRaising.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CaseManager.Common.ModelConstants;

namespace FundRaising.Data.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(PermissionConstants.DescriptionMaxLength);
        }
    }
}
