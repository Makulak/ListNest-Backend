using ListNest;
using ListNest.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ListNestTests.Configuration
{
    public class ListNestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configure only things, that must be the same in all tests
                var dbContext = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ListNestDbContext>));

                if (dbContext != null)
                    services.Remove(dbContext);
            });
        }
    }
}
