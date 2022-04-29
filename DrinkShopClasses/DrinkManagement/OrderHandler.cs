using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class OrderHandler
    {
        public List<OrderDetail> GetAllOrders()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.OrderDetails select c).ToList();
            }
        }

        public List<Order> GetOrders()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Orders.Include(m => m.OrderDetails).Include(m => m.OrderStatus) select c).ToList();
            }
        }

        public List<Order> GetOrdersDetails(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from s in db.Orders
                        .Include(t => t.OrderDetails)
                        where s.Id == id
                        select s).ToList();
            }
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.OrderDetails where c.Id == id select c).FirstOrDefault();
            }
        }

        public OrderStatus GetOrderStatusById(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.OrderStatuses where c.Id == id select c).FirstOrDefault();
            }
        }


        public List<OrderDetail> GetAllBuyedDrink(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.OrderDetails where c.Id == id select c).ToList();
            }
        }
    }
}
