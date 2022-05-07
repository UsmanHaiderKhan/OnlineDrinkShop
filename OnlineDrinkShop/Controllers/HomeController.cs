using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkShopClasses;
using DrinkShopClasses.DrinkManagement;
using DrinkShopClasses.UserMangement;
using OnlineDrinkShop.Models;

namespace OnlineDrinkShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.products = ModelHelper.ProductSummeryList(new DrinkHandler().GetLatestDrinks(6));
            return View();
        }
        [HttpGet]
        public ActionResult ByCategory(int id)
        {
            ViewBag.ByCategory = ModelHelper.ProductSummeryList(new DrinkHandler().GetDrinksByCategory(new Category { Id = id }));
            Category mb = new DrinkHandler().GetCategoryById(id);
            return View(mb);
        }

        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            List<Drink> drink = new DrinkHandler().GetDrinkbyId(id);
            return View(drink);
        }
        public ViewResult Search(string searchString)
        {
            DrinkShopContext db = new DrinkShopContext();
            string _searchString = searchString;
            IEnumerable<Drink> drinks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                drinks = db.Drinks.OrderBy(p => p.Id);
            }
            else
            {
                drinks = db.Drinks.Where(p => p.Name.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Home/index.cshtml", new DrinkListViewModel() { Drinks = drinks, CurrentCategory = "All categories" });
        }

        [HttpGet]
        public ActionResult ViewOrderDetails()
        {
            User user = (User)Session[WebUtils.CurrentUser];
            DrinkShopContext db = new DrinkShopContext();
            List<Order> orders;
            using (db)
            {
                orders = (from c in db.Orders
                        .Include(m => m.OrderStatus)
                        .Include(m => m.OrderDetails)
                          where c.BuyerName == user.FullName
                          where c.FullAddress == user.FullAddress
                          where c.EmailAddress == user.Email
                          select c).ToList();
            }
            return View(orders);
        }

    }
}