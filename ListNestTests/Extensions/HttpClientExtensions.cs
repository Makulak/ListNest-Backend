﻿using PotatoServer.ViewModels;
using PotatoServer.ViewModels.Core.User;
using PotatoServerTestsCore.Exceptions;
using PotatoServerTestsCore.Extensions;
using System.Net.Http;
using System.Threading.Tasks;

namespace ListNestTests.Extensions
{
    public static class HttpClientExtensions
    {
        public async static Task<string> GetAdminTokenAsync(this HttpClient client)
        {
            return await client.GetUserTokenAsync("admin@admin.pl", "Admin");
        }

        public async static Task<string> GetUserTokenAsync(this HttpClient client, string email, string password)
        {
            var loginVm = new UserSignInVm
            {
                Email = email,
                Password = password
            };

            var response = await client.DoPostAsync<TokenVmResult>("api/auth/sign-in", loginVm);

            if (response.IsSuccessStatusCode)
                return response.Value.Token;
            else
                throw new TestExecutionException($"{response.StatusCode}: {response.ValueString}");
        }
    }
}
