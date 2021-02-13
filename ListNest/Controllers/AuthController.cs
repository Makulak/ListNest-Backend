using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PotatoServer;
using PotatoServer.Controllers;
using PotatoServer.Database.Models;

namespace ListNest.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseAuthController<PotatoUser>
    {
        public AuthController(UserManager<PotatoUser> userManager, IStringLocalizer<SharedResources> localizer, IConfiguration configuration) : base(userManager, localizer, configuration)
        {
        }
    }
}
