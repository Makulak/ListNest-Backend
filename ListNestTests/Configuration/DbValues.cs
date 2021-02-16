using ListNest.Database.Models;
using System;

namespace ListNestTests.Configuration
{
    class DbValues
    {
        private static readonly DateTime defaultDateTime = DateTime.Parse("01-01-2021");

        public static ListNestUser[] Profiles = new[]
        {
            new ListNestUser { Email = "1@mail.com", UserName = "user1" },
            new ListNestUser { Email = "2@mail.com", UserName = "user2" }
        };

        public static List[] Lists = new[]
        {
            new List { Id = 1, Name="List1", Created = defaultDateTime },
            new List { Id = 2, Name="List2", Created = defaultDateTime },
            new List { Id = 3, Name="List3", Created = defaultDateTime,
                Changed = DateTime.Parse("02-01-2021"), IsDeleted = true }
        };

        public static ListItem[] ListItems = new[]
        {
            new ListItem { Id=1, ListId=1, Name="Item1", Quantity=1, Created = defaultDateTime },
            new ListItem { Id=2, ListId=1, Name="Item2", Quantity=2, Created = defaultDateTime },
            new ListItem { Id=3, ListId=1, Name="Item3", Quantity=3, Created = defaultDateTime,
                Changed = DateTime.Parse("02-01-2021"), IsDeleted = true }
        };
    }
}
