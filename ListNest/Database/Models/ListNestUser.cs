using PotatoServer.Database.Models;
using System.Collections.Generic;

namespace ListNest.Database.Models
{
    public class ListNestUser : PotatoUser
    {
        public ICollection<UserList> AssignedLists { get; set; }
    }
}
