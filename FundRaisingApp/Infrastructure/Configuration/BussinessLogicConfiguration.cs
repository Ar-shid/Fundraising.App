using FundRaising.Services.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace FundRaisingApp.Infrastructure.Configuration
{
    public static class BussinessLogicConfiguration
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            AddServices(services);

            return services;
        }
        private static void AddServices(IServiceCollection services) =>
           services
               .AddTransient<IAuthService, AuthService>();
    }
}
