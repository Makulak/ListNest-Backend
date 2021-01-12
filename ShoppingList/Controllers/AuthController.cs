using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PotatoServer;
using PotatoServer.Controllers;
using PotatoServer.Database.Models;

namespace ShoppingListApp.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseAuthController<User>
    {
        public AuthController(UserManager<User> userManager, IStringLocalizer<SharedResources> localizer, IConfiguration configuration) : base(userManager, localizer, configuration)
        {
        }
    }
}
