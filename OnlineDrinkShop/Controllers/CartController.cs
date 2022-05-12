using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineDrinkShop.Models;

namespace OnlineDrinkShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public int Add(ShoppingCartItem item)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart == null) cart = new ShoppingCart();
            cart.Add(item);
            Session.Add(WebUtils.Cart, cart);
            return cart.NumberOfItems;
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart != null)
            {
                cart.Remove(id);
                Session.Add(WebUtils.Cart, cart);
            }
            return RedirectToAction("ViewCart");
        }

        public int Update()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (cart != null)
            {
                cart.Update(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["qty"]));
                Session.Add(WebUtils.Cart, cart);
            }
            return cart.NumberOfItems;
        }

        [HttpGet]
        public ActionResult ViewCart()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.GrandTotal = cart.GrandTotal;
            return View();
        }


        public int ItemsCount()
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];
            return (cart == null) ? 0 : cart.NumberOfItems;
        }

    }
}