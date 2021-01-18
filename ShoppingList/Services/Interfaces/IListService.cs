using ListNest.Database.Models;
using System.Threading.Tasks;

namespace ListNest.Services.Interfaces
{
    public interface IListService
    {
        Task<List> GetAsync(int listId, string userId);
    }
}
