using FundRaising.Common.Extensions;
using FundRaising.Data;
using FundRaising.Data.Enums;
using FundRaising.Data.Models;
using FundRaising.DTO.AuthModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Services.Auth
{
    public class AuthService : IAuthService
    {

        protected readonly FundRaisingDBContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(FundRaisingDBContext context, UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
        }

        public async Task<IdentityResult> CreateUser(RegisterViewModel model)
        {
            var user = new ApplicationUser
            { 
                UserName = model.Email, 
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.LastName,
                LastName = model.LastName,
                EmailConfirmed = true,
                ActiveStatus = ActiveStatus.Active,
                Initials = model.FirstName.First().ToString() +""+ model.LastName.First().ToString(),
                CreatedOn = DateTime.Now.ToUniversalTime()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!model.Roles.Any())
                {
                    model.Roles = new string[] { EnumExtensions.GetDisplayAttributeName(BaseRoles.Organizer) };
                }
                AddUserRoles(model.Roles, user.Id);
            }
            return result;
        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
           var result = await _userManager.FindByEmailAsync(email);
            return result;
        }
        public async Task<SignInResult> CheckUserAuthentication(ApplicationUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }
        public bool AddUserRoles(string[] roles, string userId)
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
