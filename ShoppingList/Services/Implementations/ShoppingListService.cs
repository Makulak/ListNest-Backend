using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
using ShoppingListApp.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Services.Implementations
{
    public class ShoppingListService : IShoppingListService
    {
        public ShoppingListDbContext _dbContext { get; }

        public ShoppingListService(ShoppingListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingList> GetAsync(int shoppingListId, string userId)
        {
            return await _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Users)
                    .ThenInclude(userShoppingList => userShoppingList.User)
                .Where(shoppingList => !shoppingList.IsDeleted &&
                                       shoppingList.Id == shoppingListId &&
                                       shoppingList.Users.Any(user => user.UserId == userId))
                .SingleOrDefaultAsync();
        }
    }
}
