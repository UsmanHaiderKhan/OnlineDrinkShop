using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int Qauntity { get; set; }
        public long Amount { get; set; }
    }
}
