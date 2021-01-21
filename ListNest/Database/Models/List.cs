using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models;
using System;
using System.Collections.Generic;

namespace ListNest.Database.Models
{
    public class List : IBaseModel
    {
        public string Name { get; set; }
        public ICollection<ListItem> Items { get; set; }
        public ICollection<UserList> Users { get; set; }

        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.HasKey(list => list.Id);
            builder.Property(list => list.Name).HasMaxLength(128).IsRequired();
        }
    }
}
