using ListNest;
using ListNest.Database;
using ListNest.Database.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServerTestsCore.Builders;

namespace ListNestTests.Configuration
{
    internal class ListNestAppBuilder : AppBuilder<Startup, ListNestDbContext, ListNestUser>
    {
        private readonly DataSeeder _seeder;

        public ListNestAppBuilder(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _seeder = new DataSeeder();
        }

        public ListNestAppBuilder CreateSampleDataset()
        {
            CreateUsers(DbValues.Users, DbValues.DefaultPassword);
            AddAction(() => _seeder.CreateSampleDataset(_dbContext));
            
            return this;
        }
    }
}
