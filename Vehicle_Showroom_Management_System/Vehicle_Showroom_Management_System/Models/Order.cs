using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Showroom_Management_System.Models
{
    //Don hang
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get;set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get;set;}
        public DateTime? Created_At { get; set; }
        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
    }
}