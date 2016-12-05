using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatin.Models
{
    public class Room
    {
    
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
