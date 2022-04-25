using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShopClasses.DrinkManagement
{
    public class DrinkHandler
    {
        public List<Drink> GetLatestDrinks(int counter)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from m in db.Drinks
                        .Include(m => m.Category)
                        .Include(m => m.ImageUrl)
                        orderby m.Name descending
                        select m).Take(counter).ToList();
            }
        }
        public List<Drink> GetDrinksByCategory(Category category)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return
                    (from m in
                            db.Drinks
                                .Include(m => m.Category)
                                .Include(m => m.ImageUrl)
                     where m.Category.Id == category.Id
                     select m).ToList();
            }
        }
        public Category GetCategoryById(int id)
        {
            using (DrinkShopContext db = new DrinkShopContext())
            {
                return (from b in db.Categories
                        where b.Id == id
                        select b).FirstOrDefault();
            }
        }
        public Order GetOrderById(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Orders.Include(m => m.OrderDetails) where c.Id == id select c).FirstOrDefault();
            }
        }
        public List<Drink> GetDrinkbyId(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Drinks.Include(m => m.ImageUrl).Include(m => m.Category) where c.Id == id select c).ToList();
            }
        }

        public List<Drink> GetAllDrinks()
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Drinks.Include(m => m.Category).Include(m => m.ImageUrl) select c).ToList();
            }

        }

        public Drink GetSingleDrink(int id)
        {
            DrinkShopContext db = new DrinkShopContext();
            using (db)
            {
                return (from c in db.Drinks.Include(m => m.Category).Include(m => m.ImageUrl) where c.Id == id select c)
                    .FirstOrDefault();
            }
        }
    }
}
