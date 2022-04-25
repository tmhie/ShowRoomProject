using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Showroom_Management_System.Models
{
    //Loai san pham
    [Table("Categorys")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int Orders { get;set; }
        public string Describe { get; set; }

        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Update_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
    }
}