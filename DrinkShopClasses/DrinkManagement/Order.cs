using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerName { get; set; }
        public string EmailAddress { get; set; }
        public string FullAddress { get; set; }
        public bool IsActive { get; set; }
        public double Phone { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
