using Microsoft.EntityFrameworkCore;
using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ListNest.Services.Implementations
{
    public class ListItemService : IListItemService
    {
        public ListNestDbContext _dbContext { get; }

        public ListItemService(ListNestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListItem> GetAsync(int listItemId)
        {
            return await _dbContext.ListItems
                .Where(list => !list.IsDeleted && list.Id == listItemId)
                .SingleOrDefaultAsync();
        }
    }
}
