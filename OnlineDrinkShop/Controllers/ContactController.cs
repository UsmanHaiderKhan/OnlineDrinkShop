using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkShopClasses;
using DrinkShopClasses.UserMangement;

namespace OnlineDrinkShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(ContactUs contactus)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                db.Contacts.Add(contactus);
                db.SaveChanges();
            }
            return RedirectToAction("Send");
        }

        public ActionResult Send()
        {
            return View();
        }
    }
}