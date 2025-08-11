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
    public interface IAuthService
    {
        Task<IdentityResult> CreateUser(RegisterViewModel model);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<SignInResult> CheckUserAuthentication(ApplicationUser user, string password);
    }
}
