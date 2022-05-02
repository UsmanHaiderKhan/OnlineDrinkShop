using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkShopClasses.DrinkManagement;
using DrinkShopClasses.UserMangement;

namespace DrinkShopClasses
{
    public class DrinkShopContext : DbContext
    {
        public DrinkShopContext() : base("name=constr")
        {

        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImages> ProductImageses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }

    }
}
