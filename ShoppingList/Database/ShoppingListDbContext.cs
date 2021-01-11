using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using PotatoServer.Database.Models;

namespace ShoppingList.Database
{
    public class ShoppingListDbContext : CoreDatabaseContext<User>
    {
        public ShoppingListDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
