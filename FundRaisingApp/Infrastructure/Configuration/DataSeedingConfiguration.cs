using FundRaising.Data;
using FundRaising.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FundRaisingApp.Infrastructure.Configuration
{
    public static class DataSeedingConfiguration
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                FundRaisingDBContext dbContext = serviceScope.ServiceProvider.GetRequiredService<FundRaisingDBContext>();
               
                try
                {
                     new FundRaisingSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                }
                catch (Exception ex) 
                {

                    throw;
                }
               
            }

            return app;
        }
        
    }
}
