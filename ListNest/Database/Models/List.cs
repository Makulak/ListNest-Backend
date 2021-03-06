﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ListNest.Database.Models
{
    public class List : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ListItem> Items { get; set; }
        public ICollection<UserList> AssignedUsers { get; set; }
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
