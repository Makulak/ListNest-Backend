using ListNest.Database.Models;
using ListNest.ViewModels;
using PotatoServer.Helpers.Extensions;
using System.Threading.Tasks;

namespace ListNest.Hubs
{
    public partial class ListNestHub
    {
        public async Task EnterBoard(int boardId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Board_{boardId}");
        }

        public async Task LeaveBoard(int boardId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Board_{boardId}");
        }

        public async Task GetBoards(int? skip, int? take)
        {
            var boards = await _boardService.GetAsync(UserIdentityId, skip, take);

            await Clients.Caller.UpdateBoardsAsync(_mapper.MapPagedViewModel<Board, BoardApi>(boards));
        }
    }
}
