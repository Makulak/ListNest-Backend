using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Exceptions;
using PotatoServer.Helpers.Extensions;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
using ShoppingListApp.Services.Interfaces;
using ShoppingListApp.ViewModels;
using ShoppingListApp.ViewModels.Input;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingListApp.Controllers
{
    [Route("api/shopping-lists")]
    [ApiController]
    public class ShoppingListController : Controller
    {
        private readonly ShoppingListDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(ShoppingListDbContext dbContext,
            IMapper mapper,
            IShoppingListService shoppingListService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingLists(int? skip, int? take)
        {
            var userId = User.FindFirstValue("UserId");

            var shoppingLists = await _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Users)
                    .ThenInclude(userShoppingList => userShoppingList.User)
                .Where(shoppingList => !shoppingList.IsDeleted && shoppingList.Users.Any(user => user.UserId == userId))
                .GetPagedAsync<ShoppingList, ShoppingListVm>(_mapper, skip, take);

            return Ok(shoppingLists);
        }

        [HttpGet("{shoppingListId}")]
        public async Task<IActionResult> GetShoppingList(int shoppingListId)
        {
            var userId = User.FindFirstValue("UserId");
            var shoppingList = await _shoppingListService.GetAsync(shoppingListId, userId);

            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            return Ok(_mapper.Map<ShoppingListVm>(shoppingList));
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingList([FromBody] ShoppingListCreateVm shoppingListVm)
        {
            var userId = User.FindFirstValue("UserId");
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListVm);
            // Adding current user
            shoppingList.Users.Add(new UserShoppingList { UserId = userId });

            var addedShoppingList = await _dbContext.ShoppingLists.AddAsync(shoppingList);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<ShoppingListVm>(addedShoppingList.Entity));
        }

        [HttpDelete("{shoppingListId}")]
        public async Task<IActionResult> DeleteShoppingList(int shoppingListId)
        {
            var userId = User.FindFirstValue("UserId");
            var shoppingList = await _shoppingListService.GetAsync(shoppingListId, userId);

            if (shoppingList == null)
                throw new NotFoundException("Shopping list does not exist, or You don't have permissions to view it."); // TODO: Message

            shoppingList.IsDeleted = true;

            _dbContext.Update(shoppingList);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
