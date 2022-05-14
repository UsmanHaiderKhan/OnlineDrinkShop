using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineDrinkShop.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> Items { get; }


        public void Add(ShoppingCartItem newItem)
        {
            ShoppingCartItem itemFound = Items.Find(i => i.Id == newItem.Id);

            if (itemFound == null)
            {
                Items.Add(newItem);
                Items.TrimExcess();
            }
            else
            {
                itemFound.Quantity += newItem.Quantity;
            }
        }

        internal void Remove(int id)
        {
            Items.RemoveAt(Items.FindIndex(i => i.Id == id));
        }

        public int NumberOfItems
        {
            get
            {
                return Items.Count();
            }
        }

        internal void Update(int id, int qty)
        {
            ShoppingCartItem itemFound = Items.Find(i => i.Id == id);
            if (itemFound != null)
            {
                itemFound.Quantity = qty;
            }
        }
        public float Calculate(IEnumerable<ShoppingCartItem> items)
        {
            return items.Sum(cartItem => (cartItem.Price * cartItem.Quantity));
        }

        public float GrandTotal
        {
            get { return Calculate(Items); }
        }
    }
}