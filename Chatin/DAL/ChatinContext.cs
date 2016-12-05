using Chatin.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Chatin.DAL
{
    public class ChatinContext : DbContext
    {
        public ChatinContext() : base("ChatinContext")
        {

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }


}