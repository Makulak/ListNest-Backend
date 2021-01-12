using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PotatoServer.Helpers.Extensions;
using ShoppingListApp.Database;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = _dbContext.ShoppingLists
                .Include(shoppingList => shoppingList.Users)
                .Where(shoppingList => shoppingList.Users.Any(user => user.UserId == userId));

            var shoppingLists = await query.GetPagedAsync<ShoppingList, ShoppingListVm>(_mapper, skip, take);

            return Ok(shoppingLists);
        }
    }
}
