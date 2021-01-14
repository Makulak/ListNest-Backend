using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
using ShoppingListApp.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Services.Implementations
{
    public class ShoppingListItemService : IShoppingListItemService
    {
        public ShoppingListDbContext _dbContext { get; }

        public ShoppingListItemService(ShoppingListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingListItem> GetAsync(int shoppingListItemId)
        {
            return await _dbContext.ShoppingListItems
                .Where(shoppingList => !shoppingList.IsDeleted &&
                                       shoppingList.Id == shoppingListItemId)
                .SingleOrDefaultAsync();
        }
    }
}
