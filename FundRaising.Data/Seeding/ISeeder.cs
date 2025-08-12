using System;
using System.Threading.Tasks;

namespace FundRaising.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(FundRaisingDBContext dbContext, IServiceProvider serviceProvider);
    }
}
