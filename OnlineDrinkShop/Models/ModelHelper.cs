using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrinkShopClasses.DrinkManagement;

namespace OnlineDrinkShop.Models
{
    public class ModelHelper
    {
        public static List<SelectListItem> ToSelectItemList(dynamic values)

        {
            List<SelectListItem> templist = null;
            if (values != null)
            {
                templist = new List<SelectListItem>();
                foreach (var v in values)
                {
                    templist.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
                }
                templist.TrimExcess();
            }
            return templist;
        }

        public static List<ProductSummeryModel> ProductSummeryList(IEnumerable<Drink> drinkes)
        {
            List<ProductSummeryModel> temp = new List<ProductSummeryModel>();
            if (drinkes != null)
            {
                temp.AddRange(drinkes.Select(m => ToProductSummary(m)));
                temp.TrimExcess();
            }
            return temp;
        }
        public static ProductSummeryModel ToProductSummary(Drink drink)
        {
            return new ProductSummeryModel
            {
                Id = drink.Id,
                Name = drink.Name,
                Price = drink.Price,
                ShortDescription = drink.ShortDescription,
                ImageUrl = (drink.ImageUrl.Count > 0) ? drink.ImageUrl.First().Url : null
            };
        }
    }
}