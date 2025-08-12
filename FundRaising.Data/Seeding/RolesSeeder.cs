using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static FundRaising.Common.ModelConstants;

namespace FundRaising.Data.Seeding
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(FundRaisingDBContext dbContext, IServiceProvider serviceProvider)
        {
            var permissions = dbContext.Permissions.AsNoTracking().ToList();
            RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await SeedRoleAsync(roleManager, dbContext, Roles.Administrator, permissions);
            await SeedRoleAsync(roleManager, dbContext, Roles.SalePerson, permissions);
            await SeedRoleAsync(roleManager, dbContext, Roles.Organizer, permissions);
            await SeedRoleAsync(roleManager, dbContext, Roles.Participant, permissions);
        }

        private async Task SeedRoleAsync(RoleManager<Role> roleManager, FundRaisingDBContext dbContext, string roleName, List<Permission> permissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var newRole = new Role { Name = roleName };
                ICollection<Permission> rolePermissions = GetRolePremissions(roleName, permissions);
                newRole.Permissions = rolePermissions.Select(p => new RolePermission
                {
                    PermissionId = p.Id
                })
                .ToList();

                IdentityResult result = await roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

            }
            else
            {
                ICollection<Permission> rolePermissions = GetRolePremissions(roleName, permissions);

                foreach (Permission permission in rolePermissions)
                {
                    if(!dbContext.RolePermissions.Any(r => r.RoleId == role.Id && r.PermissionId == permission.Id))
                    {
                        await dbContext.RolePermissions.AddAsync(new RolePermission
                        {
                            PermissionId = permission.Id,
                            RoleId = role.Id
                        });
                    }
                }
            }
        }

        private ICollection<Permission> GetRolePremissions(string roleName, List<Permission> permissions)
        {
            if (roleName == Roles.Administrator)
            {
                return permissions;
            }
            else if (roleName == Roles.SalePerson)
            {
                return permissions.Where(p =>
                    p.Type != PermissionType.AddEditUser)
                    .ToList();
            }
            else if (roleName == Roles.Organizer)
            {
                return permissions.Where(p =>
                    p.Type == PermissionType.AddEditGroup ||
                    p.Type == PermissionType.InviteParticipant)
                    .ToList();
            }

            return new List<Permission>();
        }
    }
}
