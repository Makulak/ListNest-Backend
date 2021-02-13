using ListNest;
using ListNest.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using PotatoServerTestsCore.Builders;

namespace ListNestTests.Configuration
{
    internal class ListNestAppBuilder : AppBuilder<Startup, ListNestDbContext>
    {
        public ListNestAppBuilder(WebApplicationFactory<Startup> factory) : base(factory) { }
    }
}
