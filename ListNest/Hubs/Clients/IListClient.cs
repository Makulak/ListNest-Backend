using PotatoServer.ViewModels.Core;
using ListNest.ViewModels;
using System.Threading.Tasks;

namespace ListNest.Hubs.Clients
{
    public interface IListNestClient
    {
        Task UpdateBoardsAsync(PagedModel<BoardApi> listItems);

        Task UpdateListItemsAsync(PagedModel<ListItemApi> listItems);
        Task AddListItemAsync(ListItemApi listItem);
        Task UpdateListItemAsync(ListItemApi listItem);
        Task DeleteListItemAsync(int laistItemId);
    }
}
