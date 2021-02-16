﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListNest.Database.Models
{
    public class UserList
    {
        public ListNestUser User { get; set; }
        public List List { get; set; }

        public string UserId { get; set; }
        public int ListId { get; set; }
    }

    public class UserListConfiguration : IEntityTypeConfiguration<UserList>
    {
        public void Configure(EntityTypeBuilder<UserList> builder)
        {
            builder.HasKey(userList => new { userList.UserId, userList.ListId });
            builder.Property(userList => userList.ListId).IsRequired();
            builder.Property(userList => userList.UserId).IsRequired();
        }
    }
}
