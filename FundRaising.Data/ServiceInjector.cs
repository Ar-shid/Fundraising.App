using FundRaising.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaising.Data
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<FundRaisingDBContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<FundRaisingDBContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>()
        .AddUserManager<UserManager<ApplicationUser>>();

            return services;
        }
    }
}
