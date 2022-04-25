using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vehicle_Showroom_Management_System.Models
{
    public class ShowRoomDbContext : DbContext
    {
        public ShowRoomDbContext() : base("name = ChuoiKN") { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}