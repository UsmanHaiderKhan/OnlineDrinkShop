using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class CategoryHandler
    {
        DrinkShopContext db = new DrinkShopContext();

        public List<Category> GetAllCategories()
        {
            using (db)
            {
                return (from c in db.Categories select c).ToList();
            }
        }
    }
}
