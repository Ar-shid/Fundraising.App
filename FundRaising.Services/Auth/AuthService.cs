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

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                Initials = model.FirstName.First().ToString() +""+ model.LastName.First().ToString(),
                CreatedOn = DateTime.Now.ToUniversalTime()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
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
    }
}
