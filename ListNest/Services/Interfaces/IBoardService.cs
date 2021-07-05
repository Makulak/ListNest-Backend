using ListNest.Database.Models;
using PotatoServer.ViewModels.Core;
using System.Threading.Tasks;

namespace ListNest.Services.Interfaces
{
    public interface IBoardService
    {
        Task<PagedModel<Board>> GetAsync(string userId, int? skip, int? take);
    }
}
