using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskEFC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Column("fname"), Display(Name = "First Name"), StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Column("lname"), Display(Name = "Last Name"), StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        public int Discount { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
