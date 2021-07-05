using AutoMapper;
using ListNest.Database;
using ListNest.Services.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ListNest.Hubs
{
    public partial class ListNestHub
    {
        private readonly ListNestDbContext _dbcontext;
        private readonly IListService _listService;
        private readonly IListItemService _listItemService;
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;

        public ListNestHub(ListNestDbContext dbcontext,
            IListService listService,
            IListItemService listItemService,
            IBoardService boardService,
            IMapper mapper)
        {
            _dbcontext = dbcontext;
            _listService = listService;
            _listItemService = listItemService;
            _boardService = boardService;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        private string UserIdentityId => ((ClaimsIdentity)Context.User.Identity).Claims.SingleOrDefault(claim => claim.Type == "UserId").Value;
        private string UserIdentityName => Context.User.Identity.Name;
    }
}
