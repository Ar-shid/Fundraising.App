using FundRaising.Services.Auth;
using FundRaising.Services.CompaignService;
using FundRaising.Services.FileService;
using FundRaising.Services.GroupService;
using FundRaising.Services.ProductService;

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
               .AddTransient<ICompaignService, CompaignService>()
               .AddTransient<IGroupService, GroupService>()
               .AddTransient<IFileService, FileService>()
               .AddTransient<IProductService, ProductService>();
    }
}
