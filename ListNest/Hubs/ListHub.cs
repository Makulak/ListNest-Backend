using Microsoft.AspNetCore.SignalR;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using ListNest.Database.Models;
using ListNest.ViewModels;
using ListNest.ViewModels.Input;
using System.Linq;
using System.Threading.Tasks;
using ListNest.Hubs.Clients;

namespace ListNest.Hubs
{
    public partial class ListNestHub : Hub<IListNestClient>
    {
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
            var list = await _listService.GetSingleAsync(listId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            var items = await _listItemService.GetAsync(listId, skip, take);

            await Clients.Caller.UpdateListItemsAsync(_mapper.MapPagedViewModel<ListItem, ListItemApi>(items));
        }

        public async Task CreateListItem(ListItemCreateVm listItemVm)
        {
            var list = await _listService.GetSingleAsync(listItemVm.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            var listItem = _mapper.Map<ListItemCreateVm, ListItem>(listItemVm);

            await _listItemService.CreateAsync(listItem);

            var addedListItemVm = _mapper.Map<ListItem, ListItemApi>(listItem);

            await Clients.Group($"List_{listItemVm.ListId}").AddListItemAsync(addedListItemVm);
        }

        public async Task DeleteListItem(int listItemId)
        {
            var listItem = await _listItemService.GetSingleAsync(listItemId);
            if (listItem == null)
                throw new NotFoundException("List item does not exist, or You don't have permissions to view it."); // TODO: Message

            var list = await _listService.GetSingleAsync(listItem.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            await Clients.Group($"List_{list.Id}").DeleteListItemAsync(listItemId);
        }

        public async Task UpdateListItem(ListItemUpdateVm listItemVm)
        {
            var listItem = await _listItemService.GetSingleAsync(listItemVm.Id);
            if (listItem == null)
                throw new NotFoundException("List item does not exist, or You don't have permissions to view it."); // TODO: Message

            var list = await _listService.GetSingleAsync(listItem.ListId, UserIdentityId);
            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            listItem = _mapper.Map<ListItemUpdateVm, ListItem>(listItemVm);

            await _listItemService.UpdateAsync(listItem);

            var editedListItemVm = _mapper.Map<ListItem, ListItemApi>(listItem);

            await Clients.Group($"List_{list.Id}").UpdateListItemAsync(editedListItemVm);
        }
    }
}
