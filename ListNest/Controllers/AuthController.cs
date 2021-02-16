using ListNest.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PotatoServer;
using PotatoServer.Controllers;

namespace ListNest.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseAuthController<ListNestUser>
    {
        public AuthController(UserManager<ListNestUser> userManager, IStringLocalizer<SharedResources> localizer, IConfiguration configuration) : base(userManager, localizer, configuration)
        {
        }
    }
}
