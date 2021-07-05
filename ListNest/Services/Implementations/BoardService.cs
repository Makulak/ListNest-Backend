using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Helpers.Extensions;
using PotatoServer.ViewModels.Core;
using System.Linq;
using System.Threading.Tasks;

namespace ListNest.Services.Implementations
{
    public class BoardService : IBoardService
    {
        public ListNestDbContext _dbContext { get; }

        public BoardService(ListNestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedModel<Board>> GetAsync(string userId, int? skip, int? take)
        {
            return await _dbContext.Boards
                .Include(list => list.AssignedUsers)
                    .ThenInclude(userList => userList.User)
                .Where(board => !board.IsDeleted &&
                                 board.AssignedUsers.Any(user => user.UserId == userId))
                .GetPagedAsync(skip, take);
        }
    }
}
