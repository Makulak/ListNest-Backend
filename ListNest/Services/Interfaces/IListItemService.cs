using ListNest.Database.Models;
using PotatoServer.ViewModels.Core;
using System.Threading.Tasks;

namespace ListNest.Services.Interfaces
{
    public interface IListItemService
    {
        Task<ListItem> GetSingleAsync(int listId);

        Task<PagedModel<ListItem>> GetAsync(int listId, int? skip, int? take);

        Task CreateAsync(ListItem listItem);

        Task UpdateAsync(ListItem listItem);
    }
}
