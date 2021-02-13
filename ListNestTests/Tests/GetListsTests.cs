using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ListNestTests.Configuration;
using ListNest;
using ListNestTests.Extensions;

namespace PotatoServerTests
{
    public class GetListsTests : IClassFixture<ListNestWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetListsTests(ListNestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void SignIn_Should_SignIn()
        {
            var address = "api/core/server-settings";
            var client = new ListNestAppBuilder(_factory)
                        .CreateClient();

            var response = await client.GetUserTokenAsync("admin@admin.pl", "Admin");
        }
    }
}
