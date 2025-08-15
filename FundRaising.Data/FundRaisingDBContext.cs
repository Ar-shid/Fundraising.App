using FundRaising.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data
{
    public class FundRaisingDBContext : IdentityDbContext<ApplicationUser, Role, string, Microsoft.AspNetCore.Identity.IdentityUserClaim<string>, UserRole,
        Microsoft.AspNetCore.Identity.IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public FundRaisingDBContext(DbContextOptions<FundRaisingDBContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Compaign> Compaigns { get; set; }
        public DbSet<CompaignGroup> CompaignGroups { get; set; }
        public DbSet<CompaignImage> CompaignImages { get; set; }
        public DbSet<CompaignOrganizer> CompaignOrganizers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          //  modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FundRaisingDBContext).Assembly);
            DisableCascadeDelete(modelBuilder);
        }
        public void DisableCascadeDelete(ModelBuilder builder)
        {
            var entityTypes = builder.Model.GetEntityTypes().ToList();
            IEnumerable<IMutableForeignKey> foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys()
                    .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (IMutableForeignKey foreignKey in foreignKeys)
            {
                if (foreignKey.GetConstraintName() == "FK_TenantPracticeAreas_PracticeAreas_PracticeAreaId")
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
                }
                else
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}
