using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using ListNest.Database;
using ListNest.Database.Models;
using ListNest.Services.Interfaces;
using ListNest.ViewModels;
using ListNest.ViewModels.Input;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ListNest.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ListController : Controller
    {
        private readonly ListNestDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        public ListController(ListNestDbContext dbContext,
            IMapper mapper,
            IListService listService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _listService = listService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLists(int? skip, int? take)
        {
            var userId = User.FindFirstValue("UserId");

            var lists = await _dbContext.Lists
                .Include(list => list.AssignedUsers)
                    .ThenInclude(userList => userList.User)
                .Where(list => !list.IsDeleted && list.AssignedUsers.Any(user => user.UserId == userId))
                .GetPagedAsync(skip, take);

            return Ok(_mapper.MapPagedViewModel<List, ListApi>(lists));
        }

        [HttpGet("{listId}")]
        public async Task<IActionResult> GetList(int listId)
        {
            var userId = User.FindFirstValue("UserId");
            var list = await _listService.GetSingleAsync(listId, userId);

            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            return Ok(_mapper.Map<ListApi>(list));
        }

        [HttpPost]
        public async Task<IActionResult> CreateList([FromBody] ListCreateVm listVm)
        {
            var userId = User.FindFirstValue("UserId");
            var list = _mapper.Map<List>(listVm);
            // Adding current user
            list.AssignedUsers.Add(new UserList { UserId = userId });

            var addedList = await _dbContext.Lists.AddAsync(list);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<ListApi>(addedList.Entity));
        }

        [HttpDelete("{listId}")]
        public async Task<IActionResult> DeleteList(int listId)
        {
            var userId = User.FindFirstValue("UserId");
            var list = await _listService.GetSingleAsync(listId, userId);

            if (list == null)
                throw new NotFoundException("List does not exist, or You don't have permissions to view it."); // TODO: Message

            list.IsDeleted = true;

            _dbContext.Update(list);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
