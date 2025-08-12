using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace FundRaising.Data.Seeding
{
    public class FundRaisingSeeder : ISeeder
    {
        public async Task SeedAsync(FundRaisingDBContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            ILogger logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(FundRaisingSeeder));

            var seeders = new List<ISeeder>
            {

                new PermissionsSeeder(),
                new RolesSeeder(),
                new AdminSeeder(),
                //new ApplicationSettingsSeeder(),
            };

            foreach (ISeeder seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
       
    }
}
