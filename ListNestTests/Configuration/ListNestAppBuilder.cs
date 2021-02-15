using ListNest;
using ListNest.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServerTestsCore.Builders;

namespace ListNestTests.Configuration
{
    internal class ListNestAppBuilder : AppBuilder<Startup, ListNestDbContext>
    {
        private readonly DataSeeder _seeder;

        public ListNestAppBuilder(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _seeder = new DataSeeder();
        }

        public ListNestAppBuilder CreateSampleDataset()
        {
            _actions.Add(() => _seeder.CreateSampleDataset(_dbContext));
            return this;
        }
    }
}
