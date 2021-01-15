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
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingListApp.Hubs
{
    public partial class ShoppingListHub : Hub<IShoppingListClient>
    {
        private readonly ShoppingListDbContext _dbcontext;
        private readonly IShoppingListService _shoppingListService;
        private readonly IShoppingListItemService _shoppingListItemService;
        private readonly IMapper _mapper;

        public ShoppingListHub(ShoppingListDbContext dbcontext,
            IShoppingListService shoppingListService,
            IShoppingListItemService shoppingListItemService,
            IMapper mapper)
        {
            _dbcontext = dbcontext;
            _shoppingListService = shoppingListService;
            _shoppingListItemService = shoppingListItemService;
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
            var shoppingList = await _shoppingListService.GetAsync(shoppingListId, UserIdentityId);
            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            var items = await _dbcontext.ShoppingListItems
                .Where(item => !item.IsDeleted && item.ShoppingListId == shoppingListId)
                .GetPagedAsync<ShoppingListItem, ShoppingListItemVm>(_mapper, skip, take);

            await Clients.Caller.UpdateShoppingListItemsAsync(items);
        }

        public async Task CreateShoppingListItem(ShoppingListItemCreateVm shoppingListItemVm)
        {
            var shoppingList = await _shoppingListService.GetAsync(shoppingListItemVm.ShoppingListId, UserIdentityId);
            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            var shoppingListItem = _mapper.Map<ShoppingListItemCreateVm, ShoppingListItem>(shoppingListItemVm);

            await _dbcontext.ShoppingListItems.AddAsync(shoppingListItem);
            await _dbcontext.SaveChangesAsync();

            var addedShoppingListItemVm = _mapper.Map<ShoppingListItem, ShoppingListItemVm>(shoppingListItem);

            await Clients.Group($"ShoppingList_{shoppingListItemVm.ShoppingListId}").AddShoppingListItemAsync(addedShoppingListItemVm);
        }

        public async Task DeleteShoppingListItem(int shoppingListItemId)
        {
            var shoppingListItem = await _shoppingListItemService.GetAsync(shoppingListItemId);
            if (shoppingListItem == null)
                throw new NotFoundException("Shopping list item does not exist, or You don't have permissions to view it."); // TODO: Message

            var shoppingList = await _shoppingListService.GetAsync(shoppingListItem.ShoppingListId, UserIdentityId);
            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            shoppingListItem.IsDeleted = true;
            _dbcontext.Update(shoppingListItem);
            await _dbcontext.SaveChangesAsync();

            await Clients.Group($"ShoppingList_{shoppingList.Id}").DeleteShoppingListItemAsync(shoppingListItemId);
        }

        public async Task EditShoppingListItem(ShoppingListItemEditVm shoppingListItemVm)
        {
            var shoppingListItem = await _shoppingListItemService.GetAsync(shoppingListItemVm.Id);
            if (shoppingListItem == null)
                throw new NotFoundException("Shopping list item does not exist, or You don't have permissions to view it."); // TODO: Message

            var shoppingList = await _shoppingListService.GetAsync(shoppingListItem.ShoppingListId, UserIdentityId);
            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message


            shoppingListItem = _mapper.Map<ShoppingListItemEditVm, ShoppingListItem>(shoppingListItemVm);
            _dbcontext.Update(shoppingListItem);
            await _dbcontext.SaveChangesAsync();

            var editedShoppingListItemVm = _mapper.Map<ShoppingListItem, ShoppingListItemVm>(shoppingListItem);

            await Clients.Group($"ShoppingList_{shoppingList.Id}").UpdateShoppingListItemAsync(editedShoppingListItemVm);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        private string UserIdentityId => ((ClaimsIdentity)Context.User.Identity).Claims.SingleOrDefault(claim => claim.Type == "UserId").Value;
        private string UserIdentityName => Context.User.Identity.Name;
    }
}
