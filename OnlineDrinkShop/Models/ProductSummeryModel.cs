using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrinkShopClasses.DrinkManagement;

namespace OnlineDrinkShop.Models
{
    public class ProductSummeryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }

    }
}