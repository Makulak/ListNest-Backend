using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ShoppingListApp.Hubs
{
    public partial class ShoppingListHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        private string UserIdentityName => Context.User.Identity.Name;
    }
}
