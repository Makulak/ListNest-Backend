using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using ListNest.ViewModels;
using ListNest.ViewModels.Input;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ListNest.Hubs.Clients;

namespace ListNest.Hubs
{
    public partial class ListHub : Hub<IListItemClient>
    {
        private readonly ListNestDbContext _dbcontext;
        private readonly IListService _listService;
        private readonly IListItemService _listItemService;
        private readonly IMapper _mapper;

        public ListHub(ListNestDbContext dbcontext,
            IListService listService,
            IListItemService listItemService,
            IMapper mapper)
        {
            _dbcontext = dbcontext;
            _listService = listService;
            _listItemService = listItemService;
            _mapper = mapper;
        }

        public async Task EnterList(int listId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"List_{listId}");
        }
        public async Task LeaveList(int listId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"List_{listId}");
        }

        public async Task GetListItems(int listId, int? skip, int? take)
        {
            var list = await _listService.GetAsync(listId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            var items = await _dbcontext.ListItems
                .Where(item => !item.IsDeleted && item.ListId == listId)
                .GetPagedAsync(skip, take);

            await Clients.Caller.UpdateListItemsAsync(_mapper.MapPagedViewModel<ListItem, ListItemVm>(items));
        }

        public async Task CreateListItem(ListItemCreateVm listItemVm)
        {
            var list = await _listService.GetAsync(listItemVm.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            var listItem = _mapper.Map<ListItemCreateVm, ListItem>(listItemVm);

            await _dbcontext.ListItems.AddAsync(listItem);
            await _dbcontext.SaveChangesAsync();

            var addedListItemVm = _mapper.Map<ListItem, ListItemVm>(listItem);

            await Clients.Group($"List_{listItemVm.ListId}").AddListItemAsync(addedListItemVm);
        }

        public async Task DeleteListItem(int listItemId)
        {
            var listItem = await _listItemService.GetAsync(listItemId);
            if (listItem == null)
                throw new NotFoundException("List item does not exist, or You don't have permissions to view it."); // TODO: Message

            var list = await _listService.GetAsync(listItem.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            listItem.IsDeleted = true;
            _dbcontext.Update(listItem);
            await _dbcontext.SaveChangesAsync();

            await Clients.Group($"List_{list.Id}").DeleteListItemAsync(listItemId);
        }

        public async Task EditListItem(ListItemEditVm listItemVm)
        {
            var listItem = await _listItemService.GetAsync(listItemVm.Id);
            if (listItem == null)
                throw new NotFoundException("List item does not exist, or You don't have permissions to view it."); // TODO: Message

            var list = await _listService.GetAsync(listItem.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            listItem = _mapper.Map<ListItemEditVm, ListItem>(listItemVm);
            _dbcontext.Update(listItem);
            await _dbcontext.SaveChangesAsync();

            var editedListItemVm = _mapper.Map<ListItem, ListItemVm>(listItem);

            await Clients.Group($"List_{list.Id}").UpdateListItemAsync(editedListItemVm);
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
