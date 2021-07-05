using PotatoServer.Database.Models;
using System;

namespace ListNest.Database.Models
{
    public class BaseEntity : IBaseModel
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
