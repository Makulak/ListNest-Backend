using ShoppingListApp.Database.Models;
using System.Threading.Tasks;

namespace ShoppingListApp.Services.Interfaces
{
    public interface IShoppingListItemService
    {
        Task<ShoppingListItem> GetAsync(int shoppingListId);
    }
}
