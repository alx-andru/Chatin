using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chatin.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        
        public String Text { get; set; }
        public DateTime Timestamp { get; set; }

        //[ForeignKey("User")]
        public int? UserID { get; set; }
        public virtual User User { get; set; }

        //[ForeignKey("Room")]
        public int? RoomID { get; set; }
        public virtual Room Room { get; set; }
    }
}
