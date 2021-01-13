using ShoppingListApp.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListApp.Services.Interfaces
{
    public interface IShoppingListService
    {
        Task<ShoppingList> GetShoppingListAsync(int shoppingListId, string userId);
    }
}
