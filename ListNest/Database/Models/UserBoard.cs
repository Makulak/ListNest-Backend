using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListNest.Database.Models
{
    public class UserBoard
    {
        public ListNestUser User { get; set; }
        public Board Board { get; set; }

        public string UserId { get; set; }
        public int BoardId { get; set; }
    }

    public class UserBoardConfiguration : IEntityTypeConfiguration<UserBoard>
    {
        public void Configure(EntityTypeBuilder<UserBoard> builder)
        {
            builder.HasOne(x => x.Board)
                .WithMany(x => x.AssignedUsers)
                .HasForeignKey(x => x.BoardId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.AssignedBoards)
                .HasForeignKey(x => x.UserId);

            builder.HasKey(userList => new { userList.UserId, userList.BoardId });
            builder.Property(userList => userList.BoardId).IsRequired();
            builder.Property(userList => userList.UserId).IsRequired();
        }
    }
}
