using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListNest.Database.Models
{
    public class ListItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ListId { get; set; }
    }

    public class ListItemConfiguration : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder.HasKey(listItem => listItem.Id);
            builder.Property(listItem => listItem.Name).HasMaxLength(128).IsRequired();
            builder.Property(listItem => listItem.Description).HasMaxLength(255);
        }
    }
}
