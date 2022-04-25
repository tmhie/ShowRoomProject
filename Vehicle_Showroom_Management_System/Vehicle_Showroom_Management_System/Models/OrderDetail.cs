using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Showroom_Management_System.Models
{
    //chi tiet don hang
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Describe { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }

    }
}