using PotatoServer.ViewModels.Core;
using ListNest.ViewModels;
using System.Threading.Tasks;

namespace ListNest.Hubs.Clients
{
    public interface IListItemClient
    {
        Task UpdateListItemsAsync(PagedVmResult<ListItemVm> listItems);
        Task AddListItemAsync(ListItemVm listItem);
        Task UpdateListItemAsync(ListItemVm listItem);
        Task DeleteListItemAsync(int laistItemId);
    }
}
