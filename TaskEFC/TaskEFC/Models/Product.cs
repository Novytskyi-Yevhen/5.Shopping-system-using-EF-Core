using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskEFC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public float Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}