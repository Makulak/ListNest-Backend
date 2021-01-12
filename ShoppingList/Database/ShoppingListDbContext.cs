using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using PotatoServer.Database.Models;
using ShoppingListApp.Database.Models;

namespace ShoppingListApp.Database
{
    public class ShoppingListDbContext : CoreDatabaseContext<User>
    {
        public ShoppingListDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ShoppingListConfiguration());
            builder.ApplyConfiguration(new ShoppingListItemConfiguration());
            builder.ApplyConfiguration(new UserShoppingListConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
