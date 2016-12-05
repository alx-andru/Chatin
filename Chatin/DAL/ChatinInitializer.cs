using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Chatin.Models;

namespace Chatin.DAL
{
    public class ChatinInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ChatinContext>
    {
        protected override void Seed(ChatinContext context)
        {
            var users = new List<User>
            {
                new User {Name="Addo" },
                new User {Name="Alex" },
                new User {Name="Miro" }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var rooms = new List<Room>
            {
                new Room {Name="General", Icon="room" },
                new Room {Name="Private", Icon="lock"}
               
            };
            rooms.ForEach(r => context.Rooms.Add(r));
            context.SaveChanges();
            /*
            var messages = new List<Message>
            {
                new Message {User=1,Room=1,Text="Hi",Timestamp= DateTime.Now},
                new Message {User=1,Room=1,Text="Hi there.",Timestamp= DateTime.Now },

            };
            messages.ForEach(m => context.Messages.Add(m));
            context.SaveChanges();
            */

        }
    }
}
