using FundRaising.Common.Extensions;
using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using static FundRaising.Common.ModelConstants;

namespace FundRaising.Data.Seeding
{
	public class AdminSeeder : ISeeder
	{
		public const string Username = "root";
		public const string Password = "fundRaising@123";

        public async Task SeedAsync(FundRaisingDBContext dbContext, IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userFromDb = await userManager.FindByNameAsync(Username);

            if (userFromDb != null)
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = Username,
                FirstName = "Super",
                MiddleName = "",
                LastName = "Admin",
                Email = "root@gmail.com",
                EmailConfirmed = true,
                Initials = "AD",
                ActiveStatus = ActiveStatus.Active,
                CreatedOn = DateTime.Now.ToUniversalTime()
            };

            IdentityResult result = await userManager.CreateAsync(user, Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
            var roles = new string[] { EnumExtensions.GetDisplayAttributeName(BaseRoles.Organizer) };
        
        AddUserRoles(dbContext, roles, user.Id);
        //await userManager.AddToRoleAsync(user, Roles.Administrator);

    }
        public bool AddUserRoles(FundRaisingDBContext context, string[] roles, string userId)
        {
            bool result = false;
            var userRoles = context.UserRoles.Where(u => u.UserId == userId).ToArray();

            if (userRoles != null && userRoles.Length > 0)
            {
                context.UserRoles.RemoveRange(userRoles);
                context.SaveChanges();
            }

            foreach (string role in roles)
            {
                var roleToBeAdded = context.Roles.Where(r => r.Name == role.ToUpper()).FirstOrDefault();

                var newUserRole = new UserRole();
                //newUserRole.Role = roleToBeAdded;
                //newUserRole.User = userOfRoles;
                newUserRole.RoleId = roleToBeAdded.Id;
                newUserRole.UserId = userId;

                try
                {
                    context.UserRoles.Add(newUserRole);
                    context.SaveChanges();
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
