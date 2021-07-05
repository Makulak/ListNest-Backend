using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ListNest.Database.Models
{
    public class Board : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<List> Lists { get; set; }
        public ICollection<UserBoard> AssignedUsers { get; set; }
    }

    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(list => list.Id);
            builder.Property(list => list.Name).HasMaxLength(128).IsRequired();
        }
    }
}
