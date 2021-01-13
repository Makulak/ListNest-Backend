using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
using ShoppingListApp.Hubs.Clients;
using ShoppingListApp.Services.Interfaces;
using ShoppingListApp.ViewModels;
using ShoppingListApp.ViewModels.Input;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Hubs
{
    public partial class ShoppingListHub : Hub<IShoppingListClient>
    {
        private readonly ShoppingListDbContext _dbcontext;
        private readonly IShoppingListService _shoppingListService;
        private readonly IMapper _mapper;

        public ShoppingListHub(ShoppingListDbContext dbcontext,
            IShoppingListService shoppingListService,
            IMapper mapper)
        {
            _dbcontext = dbcontext;
            _shoppingListService = shoppingListService;
            _mapper = mapper;
        }

        public async Task EnterShoppingList(int shoppingListId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"ShoppingList_{shoppingListId}");
        }
        public async Task LeaveShoppingList(int shoppingListId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ShoppingList_{shoppingListId}");
        }

        public async Task GetShoppingListItems(int shoppingListId, int? skip, int? take)
        {
            var shoppingList = await _shoppingListService.GetShoppingListAsync(shoppingListId, UserIdentityName);

            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            var items = await _dbcontext.ShoppingListItems
                .Where(item => !item.IsDeleted && item.ShoppingListId == shoppingListId)
                .GetPagedAsync<ShoppingListItem, ShoppingListItemVm>(_mapper, skip, take);

            await Clients.Caller.UpdateShoppingListItemsAsync(items);
        }

        public async Task AddShoppingListItem(ShoppingListItemInputVm shoppingListItem)
        {
            //await Clients.Group($"ShoppingList_{shoppingListId}").SendAsync("updateShoppingListItems", items);
        }

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
