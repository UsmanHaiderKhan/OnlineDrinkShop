using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDrinkShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public int Amount
        {
            get
            {
                return (int)(Price * Quantity);
            }
        }
    }
}