using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models;
using System;
using System.Collections.Generic;

namespace ShoppingListApp.Database.Models
{
    public class ShoppingList : IBaseModel
    {
        public string Name { get; set; }
        public ICollection<ShoppingListItem> Items { get; set; }
        public ICollection<UserShoppingList> Users { get; set; }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }

    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(shoppingList => shoppingList.Id);
            builder.Property(shoppingList => shoppingList.Name).IsRequired();
        }
    }
}
