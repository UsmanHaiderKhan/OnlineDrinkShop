using DrinkShopClasses.DrinkManagement;
using System.Collections.Generic;

namespace OnlineDrinkShop.Models
{
    public class DrinkListViewModel
    {
        public string CurrentCategory { get; set; }
        public IEnumerable<Drink> Drinks { get; set; }
    }
}
