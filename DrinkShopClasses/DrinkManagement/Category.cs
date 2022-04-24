using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Drink Category")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}
