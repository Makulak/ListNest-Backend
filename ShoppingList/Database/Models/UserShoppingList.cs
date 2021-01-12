using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models;

namespace ShoppingListApp.Database.Models
{
    public class UserShoppingList
    {
        public User User { get; set; }
        public ShoppingList ShoppingList { get; set; }

        public string UserId { get; set; }
        public int ShoppingListId { get; set; }
    }

    public class UserShoppingListConfiguration : IEntityTypeConfiguration<UserShoppingList>
    {
        public void Configure(EntityTypeBuilder<UserShoppingList> builder)
        {
            builder.HasKey(userShoppingList => new { userShoppingList.UserId, userShoppingList.ShoppingListId });
            builder.Property(UserShoppingList => UserShoppingList.ShoppingListId).IsRequired();
            builder.Property(UserShoppingList => UserShoppingList.UserId).IsRequired();
        }
    }
}
