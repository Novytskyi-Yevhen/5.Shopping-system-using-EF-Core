using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskEFC.Models
{
    public class OrderDetail
    {
        [Required]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        public float Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}