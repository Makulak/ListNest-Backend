using PotatoServer.ViewModels.Core;
using ListNest.ViewModels;
using System.Threading.Tasks;

namespace ListNest.Hubs.Clients
{
    public interface IListNestClient
    {
        Task UpdateListItemsAsync(PagedVmResult<ListItemVmResult> listItems);
        Task AddListItemAsync(ListItemVmResult listItem);
        Task UpdateListItemAsync(ListItemVmResult listItem);
        Task DeleteListItemAsync(int laistItemId);
    }
}
