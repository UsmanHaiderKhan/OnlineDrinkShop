using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkShopClasses;
using DrinkShopClasses.DrinkManagement;
using DrinkShopClasses.UserMangement;

namespace OnlineDrinkShop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            return View();
        }

        public ActionResult GetAllUsers()
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            List<User> users = new UserHandler().GetUsers();
            return View(users);
        }

        public ActionResult DeleteUser(int id)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            DrinkShopContext db = new DrinkShopContext();
            User p = (from c in db.Users
                    .Include(x => x.Role)
                      where c.Id == id
                      select c).FirstOrDefault();
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult UpdateUser(int id)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });

            User user1 = new UserHandler().GetUserById(id);
            return View(user1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateUser(User u)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });

            DrinkShopContext db = new DrinkShopContext();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    User oldUser = db.Users.Find(u.Id);
                    oldUser.Role = u.Role;
                    oldUser.FullName = u.FullName;
                    oldUser.City = u.City;
                    oldUser.State = u.State;
                    oldUser.ConfirmPassword = u.ConfirmPassword;
                    oldUser.Password = u.Password;
                    oldUser.UserName = u.UserName;
                    oldUser.ImageUrl = u.ImageUrl;
                    oldUser.PhoneNo = u.PhoneNo;
                    oldUser.Email = u.Email;
                    oldUser.FullAddress = u.FullAddress;
                    // db.Entry(oldUser.Role).State = EntityState.Unchanged;
                    db.Entry(oldUser).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("GetAllUsers");
            }

            return View();

        }
        public int GetUserCount()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Users select c).Count();
            }

        }
        [HttpGet]
        public ActionResult ProductsinShop()
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            List<Drink> drinks = new DrinkHandler().GetAllDrinks();
            return View(drinks);
        }

        public ActionResult DeleteProduct(int id)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            DrinkShopContext db = new DrinkShopContext();
            Drink drink = (from c in db.Drinks
                    .Include(x => x.ImageUrl)
                    .Include(m => m.Category)
                           where c.Id == id
                           select c).FirstOrDefault();
            db.Entry(drink).State = EntityState.Deleted;
            db.SaveChanges();
            return Json("Delete", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            Drink drink = new DrinkHandler().GetSingleDrink(id);
            return View(drink);

        }

        [HttpPost]
        public ActionResult UpdateProduct(Drink drink)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                db.Entry(drink).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ProductsinShop");
        }
    }
}