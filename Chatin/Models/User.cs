using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
