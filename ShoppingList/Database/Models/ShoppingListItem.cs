using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models;
using System;

namespace ShoppingListApp.Database.Models
{
    public class ShoppingListItem : IBaseModel
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public int ShoppingListId { get; set; }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ShoppingListItemConfiguration : IEntityTypeConfiguration<ShoppingListItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            builder.HasKey(shoppingListItem => shoppingListItem.Id);
            builder.Property(shoppingListItem => shoppingListItem.Name).IsRequired();
            builder.Property(shoppingListItem => shoppingListItem.Quantity).HasDefaultValue(1).IsRequired();
        }
    }
}
