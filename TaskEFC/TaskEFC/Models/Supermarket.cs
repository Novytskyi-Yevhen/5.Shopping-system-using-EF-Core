using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskEFC.Models
{
    public class Supermarket
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}