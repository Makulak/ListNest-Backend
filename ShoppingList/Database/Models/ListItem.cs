using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models;
using System;

namespace ListNest.Database.Models
{
    public class ListItem : IBaseModel
    {
        public string Name { get; set; }
        public float Quantity { get; set; }
        public int ListId { get; set; }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ListItemConfiguration : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder.HasKey(listItem => listItem.Id);
            builder.Property(listItem => listItem.Name).IsRequired();
            builder.Property(listItem => listItem.Quantity).HasDefaultValue(1).IsRequired();
        }
    }
}
