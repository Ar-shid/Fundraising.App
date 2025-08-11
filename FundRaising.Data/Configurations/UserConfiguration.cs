using FundRaising.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static CaseManager.Common.ModelConstants;

namespace FundRaising.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.HasIndex(e => e.IsDeleted);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(UserConstants.FirstNameMaxLength);

            builder.Property(e => e.Initials)
                .HasMaxLength(UserConstants.InitialsMaxLength)
                .IsUnicode(false);


            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(UserConstants.LastNameMaxLength);

            builder.Property(e => e.MiddleName)
                .HasMaxLength(UserConstants.MiddleNameMaxLength);

           
            builder
                .HasMany(e => e.Roles)
                .WithOne(u => u.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

        }
    }
}
