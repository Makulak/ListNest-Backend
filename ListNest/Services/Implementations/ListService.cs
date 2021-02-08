using Microsoft.EntityFrameworkCore;
using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ListNest.Services.Implementations
{
    public class ListService : IListService
    {
        public ListNestDbContext _dbContext { get; }

        public ListService(ListNestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List> GetAsync(int listId, string userId)
        {
            return await _dbContext.Lists
                .Include(list => list.Users)
                    .ThenInclude(userList => userList.User)
                .Where(list => !list.IsDeleted &&
                                       list.Id == listId &&
                                       list.Users.Any(user => user.UserId == userId))
                .SingleOrDefaultAsync();
        }
    }
}
