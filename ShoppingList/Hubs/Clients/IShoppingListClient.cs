using PotatoServer.ViewModels.Core;
using ShoppingListApp.ViewModels;
using System.Threading.Tasks;

namespace ShoppingListApp.Hubs.Clients
{
    public interface IShoppingListClient
    {
        Task UpdateShoppingListItemsAsync(PagedViewModel<ShoppingListItemVm> items);
    }
}
