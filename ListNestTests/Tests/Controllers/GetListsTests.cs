using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ListNestTests.Configuration;
using ListNest;
using PotatoServerTestsCore.Extensions;
using PotatoServer.ViewModels.Core;
using ListNest.ViewModels;
using PotatoServerTestsCore.Asserts;
using ListNestTests.Extensions;

namespace PotatoServerTests.Tests.Controllers
{
    public class GetListsTests : IClassFixture<ListNestWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly string baseUrl = "api/lists";

        public GetListsTests(ListNestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void GetLists_Should_ReturnListsAssignedToUser()
        {
            // Add two users
            // Add lists
            // Assign lists to users
            // GetLists for one user
            // Check
        }

        [Fact]
        public async void GetLists_Should_ReturnEmptyList_When_UserIsNotAssignedToAnyList()
        {
            // Add user
            // GetLists
            // Check

            var client = new ListNestAppBuilder(_factory)
                        .CreateSampleDataset()
                        .CreateClient();

            var token = await client.GetUserTokenAsync(DbValues.Users[0].Email, DbValues.DefaultPassword);

            var response = await client.DoGetAsync<PagedModel<ListApi>>(baseUrl, token);
        }

        [Fact]
        public async void GetLists_Should_ReturnForbidden_When_UserIsNotLogged()
        {
            // Arrange
            var client = new ListNestAppBuilder(_factory)
                        .CreateSampleDataset()
                        .CreateClient();

            // Act
            var response = await client.DoGetAsync<PagedModel<ListApi>>(baseUrl);

            // Assert
            PotatoAssert.EqualStatusCode(System.Net.HttpStatusCode.Unauthorized, response);
        }
    }
}
