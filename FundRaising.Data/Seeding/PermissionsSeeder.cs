using FundRaising.Common.Extensions;
using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaising.Data.Seeding
{
    public class PermissionsSeeder : ISeeder
    {
       
        public async Task SeedAsync(FundRaisingDBContext dbContext, IServiceProvider serviceProvider)
        {
            Dictionary<PermissionType, string> permissionTypesByDescription = EnumExtensions.GetEnumValuesByDisplayName<PermissionType>();
            foreach (KeyValuePair<PermissionType, string> permissionTypeByDescription in permissionTypesByDescription)
            {
                PermissionType type = permissionTypeByDescription.Key;
                string description = permissionTypeByDescription.Value;

                if (!dbContext.Permissions.Any(p => p.Type == type))
                {
                    await dbContext.Permissions.AddAsync(new Permission
                    {
                        Type = type,
                        Description = description
                    });

                    dbContext.SaveChanges();
                }

                
            }
        }
    }
}
