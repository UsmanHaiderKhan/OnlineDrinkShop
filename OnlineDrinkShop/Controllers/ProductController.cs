using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DrinkShopClasses;
using DrinkShopClasses.DrinkManagement;
using DrinkShopClasses.UserMangement;
using OnlineDrinkShop.Models;

namespace OnlineDrinkShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult AddProduct()
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });

            CategoryHandler mHandler = new CategoryHandler();
            ViewBag.Category = ModelHelper.ToSelectItemList(mHandler.GetAllCategories());
            return View();
        }
        [HttpPost]

        public ActionResult AddProduct(Drink drink, FormCollection data)
        {
            User user = (User)Session[WebUtils.CurrentUser];//for  Seeing Admin
            if (!(user != null && user.IsInRole(WebUtils.Admin)))
                return RedirectToAction("Login", "Users", new { ctl = "Admin", act = "Index" });

            long uno = DateTime.Now.Ticks;
            int counter = 0;
            foreach (string fcName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fcName];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    string url = "/Content/ProductImages/" + uno + "_" + ++counter +
                                 file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string path = Request.MapPath(url);
                    //m.Images = file.FileName;
                    file.SaveAs(path);
                    drink.ImageUrl.Add(new ProductImages { Url = url, Perority = counter });
                }
            }
            drink.Category = new Category { Id = Convert.ToInt32(data["category"]) };
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                db.Drinks.Add(drink);
                db.Entry(drink.Category).State = EntityState.Unchanged;
                db.SaveChanges();
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Order()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Order(FormCollection data)
        {
            DrinkShopContext db = new DrinkShopContext();
            Order p = new Order()
            {
                BuyerName = data["BuyerName"],
                FullAddress = data["FullAddress"],
                EmailAddress = data["EmailAddress"],
                Phone = Convert.ToDouble(data["Phone"]),
                IsActive = false,
                OrderStatus = new OrderHandler().GetOrderStatusById(1)
            };
            List<OrderDetail> cartItems = new List<OrderDetail>();
            ShoppingCart cart = (ShoppingCart)Session[WebUtils.Cart];

            foreach (var i in cart.Items)
            {
                OrderDetail ci = new OrderDetail
                {
                    Id = i.Id,
                    Name = i.Name,
                    ImageUrl = i.ImageUrl,
                    Qauntity = i.Quantity,
                    Price = i.Price,
                    Amount = i.Amount
                };
                cartItems.Add(ci);
            }

            p.OrderDetails = cartItems;

            foreach (var i in cartItems)
            {
                db.OrderDetails.Add(i);
            }

            db.Orders.Add(p);
            db.SaveChanges();
            //Session.Clear();
            //Here We Sent the Email 
            string message = null;
            try
            {

                if (ModelState.IsValid)
                {
                    var sender = new MailAddress("usmanhaiderkhan4@gmail.com");
                    var reciver = new MailAddress(p.EmailAddress);
                    var password = "03349875662";

                    var p1 = new DrinkHandler().GetOrderById(p.Id);
                    if (p1 != null)
                    {
                        var sub = "---- E-Drink Shope ----";

                        var body = $"<h2 style='color:#bc4b4b'><center>---Your Buyed Drink Information Given Below --- </center></h2><br />" +
                                   $"<div><table style='width:60%;border:1px solid yellow;padding:10px; background-color: #eeeeee; margin: auto;border-collapse: collapse; transition: all 1s;'><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue''  style='border:1px solid grey;padding:10px;background-color: #eee;'><th style='border:1px solid grey;padding:10px'>Customer Name:</th><td style='border:1px solid grey;padding:10px'>{p1.BuyerName}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color: pink;'><th style='border:1px solid grey;padding:10px'>Customer Address:</th><td style='border:1px solid grey;padding:10px'>{p1.FullAddress}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color:  #eee;'><th style='border:1px solid grey;padding:10px'>Customer PhoneNo:</th><td style='border:1px solid grey;padding:10px'>{p1.Phone}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='border:1px solid grey;padding:10px;background-color: pink;'><th style='border:1px solid grey;padding:10px'>Product Name:</th><td style='border:1px solid grey;padding:10px'>{p1.OrderDetails.First().Name}</td></tr><tr onMouseOver=this.style.color = 'red''  onMouseOut=this.style.color = 'blue'' style='background-color:  #eee;border:1px solid grey;padding:10px'><th style='border:1px solid grey;padding:10px'>Product Quantity:</th><td style='border:1px solid grey;padding:10px'>{p1.OrderDetails.First().Qauntity}</td></tr><tr onMouseOver=this.style.color = 'red'' onMouseOut=this.style.color = 'blue'' style='background-color: pink;border:1px solid grey;padding:10px'><th style='border:1px solid grey;padding:10px'>Product Amount:</th><td style='border:1px solid grey;padding:10px'>{p1.OrderDetails.First().Amount}</td></tr></table></div>";
                        var smtp1 = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, password)

                        };
                        using (var mailMessage = new MailMessage(sender, reciver)
                        {
                            IsBodyHtml = true,
                            BodyEncoding = UTF8Encoding.UTF8,
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp1.Send(mailMessage);
                            message = "Your Order Details Has Been Sent By Email SuccessFully";
                        }
                    }
                    else
                    {

                        message = "Their is Some Problem in NetWork Sending Email Failed";
                    }

                    return RedirectToAction("Complete");
                }
            }
            catch (Exception e)
            {
                ViewBag.OrderMessageError = $"There Is Some Problem Here Please Try Agin{e}";
            }

            return RedirectToAction("Complete", message);

        }

        //public ActionResult EmailSendToCutomer()
        //{
        //    bool result=false;
        //    result = PlaceOrder(null);
        //    return Json(result,JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Complete(string msg)
        {
            //List<OrderDetail> orders = new OrderHandler().GetAllBuyedDrink(id);
            ViewBag.msg = msg;
            return View();
        }





    }
}