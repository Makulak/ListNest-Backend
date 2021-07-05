using Microsoft.EntityFrameworkCore;
using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using PotatoServer.ViewModels.Core;
using PotatoServer.Helpers.Extensions;

namespace ListNest.Services.Implementations
{
    public class ListItemService : IListItemService
    {
        public ListNestDbContext _dbContext { get; }

        public ListItemService(ListNestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ListItem> GetSingleAsync(int listItemId)
        {
            return await _dbContext.ListItems
                .Where(list => !list.IsDeleted && list.Id == listItemId)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedModel<ListItem>> GetAsync(int listItemId, int? skip, int? take)
        {
            return await _dbContext.ListItems
                .Where(list => !list.IsDeleted && list.Id == listItemId)
                .GetPagedAsync(skip, take);
        }

        public async Task CreateAsync(ListItem listItem)
        {
            await _dbContext.ListItems.AddAsync(listItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ListItem listItem)
        {
            listItem.IsDeleted = true;
            _dbContext.Update(listItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ListItem listItem)
        {
            _dbContext.Update(listItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
