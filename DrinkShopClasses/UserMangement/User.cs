using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DrinkShopClasses.UserMangement
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is Required...!")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Phone#")]
        [DataType(DataType.PhoneNumber)]
        public long PhoneNo { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email Not Varified !!")]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }





        public Role Role { get; set; }
        public bool IsInRole(int id)
        {
            return (Role != null && Role.Id == id);
        }
    }
}
