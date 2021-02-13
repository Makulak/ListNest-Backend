using Microsoft.EntityFrameworkCore;
using PotatoServer.Database;
using PotatoServer.Database.Models;
using ListNest.Database.Models;

namespace ListNest.Database
{
    public class ListNestDbContext : BaseDbContext<PotatoUser>
    {
        public ListNestDbContext(DbContextOptions options) : base(options) { }

        public DbSet<List> Lists { get; set; }
        public DbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ListConfiguration());
            builder.ApplyConfiguration(new ListItemConfiguration());
            builder.ApplyConfiguration(new UserListConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
