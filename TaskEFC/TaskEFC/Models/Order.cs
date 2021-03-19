
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskEFC.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Column("customer_id"), Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Column("supermarket_id"), Display(Name = "SuperMarket")]
        public int SupermarketId { get; set; }

        [DataType(DataType.Date)]
        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public Supermarket Supermarket { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
