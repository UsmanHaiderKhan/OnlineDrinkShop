using DrinkShopClasses;
using DrinkShopClasses.DrinkManagement;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OnlineDrinkShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReciveOrders()
        {
            List<OrderDetail> orders = new OrderHandler().GetAllOrders();
            return View(orders);
        }
        public int GetOrdersCount()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from o in db.OrderDetails select o).Count();
            }
        }

        public ActionResult Orderby()
        {
            List<Order> orders = new OrderHandler().GetOrders();
            return View(orders);
        }
        public ActionResult DeleteOrdersBy(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            Order p = (from c in db.Orders.Include(x => x.OrderDetails) where c.Id == id select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);


        }
        public ActionResult OrderView(int id)
        {
            OrderHandler ph = new OrderHandler();
            List<Order> pl = ph.GetOrders();
            ViewBag.POrders = pl;
            List<Order> orders = new OrderHandler().GetOrdersDetails(id);
            return View(orders);
        }
        public ActionResult DeleteOrders(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            OrderDetail p = (from c in db.OrderDetails where c.Id == id select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);


        }


        public int GetOrderByCount()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Orders select c).Count();
            }
        }

        [HttpGet]
        public ActionResult UpdateOrder(int id)
        {
            OrderDetail orderDetail = new OrderHandler().GetOrderDetailById(id);
            return View(orderDetail);
        }
        [HttpPost]
        public ActionResult UpdateOrder(OrderDetail orderDetail)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ReciveOrders");
        }
    }
}
