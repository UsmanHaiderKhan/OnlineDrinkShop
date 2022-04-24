using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class Drink
    {
        public int Id { get; set; }
        [Display(Name = "Drink Name")]
        public string Name { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public List<ProductImages> ImageUrl { get; set; }
        public bool IsPreferredDrink { get; set; }
        public bool InStock { get; set; }
        public virtual Category Category { get; set; }

        public Drink()
        {
            ImageUrl = new List<ProductImages>();
        }
    }

}
