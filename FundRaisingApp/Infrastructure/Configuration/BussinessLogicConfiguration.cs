using FundRaising.Services.Auth;
using FundRaising.Services.CompaignService;

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
               .AddTransient<IAuthService, AuthService>()
               .AddTransient<ICompaignService, CompaignService>();
    }
}
