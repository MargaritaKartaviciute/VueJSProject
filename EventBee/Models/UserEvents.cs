using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Models
{
    public class UserEvents
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
