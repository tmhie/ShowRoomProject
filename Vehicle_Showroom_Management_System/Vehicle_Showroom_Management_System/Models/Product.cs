using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vehicle_Showroom_Management_System.Models
{
    [Table("Products")]
    //San pham
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Describe { get; set; }
        public string Img { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceSale { get;set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
    }
}