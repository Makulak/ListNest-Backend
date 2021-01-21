using ListNest.Database.Models;
using System.Threading.Tasks;

namespace ListNest.Services.Interfaces
{
    public interface IListItemService
    {
        Task<ListItem> GetAsync(int listId);
    }
}
