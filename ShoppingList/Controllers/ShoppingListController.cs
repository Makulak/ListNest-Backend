using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Helpers.Extensions;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
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

        public ShoppingListController(ShoppingListDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetShoppingLists(int? skip, int? take)
        {
            var userId = User.FindFirstValue("UserId");

            var query = _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Users)
                .Where(shoppingList => shoppingList.Users.Any(user => user.UserId == userId));

            var shoppingLists = await query.GetPagedAsync<ShoppingList, ShoppingListVm>(_mapper, skip, take);

            return Ok(shoppingLists);
        }

        [HttpPost]
        public async Task<ActionResult> CreateShoppingList([FromBody] ShoppingListInputVm shoppingListVm)
        {
            var userId = User.FindFirstValue("UserId");
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListVm);
            // Adding current user
            shoppingList.Users.Add(new UserShoppingList { UserId = userId });

            var addedShoppingList = await _dbContext.ShoppingLists.AddAsync(shoppingList);
            await _dbContext.SaveChangesAsync();

            return Ok(addedShoppingList.Entity);
        }
    }
}
