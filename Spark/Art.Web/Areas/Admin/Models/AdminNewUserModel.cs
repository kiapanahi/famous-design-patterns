using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class AdminNewUserModel
    {
        public int? Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Min password length is 7 characters")]
        public string Password { get; set; }

        // these values are used when updating a user

        public DateTime SignupDate { get; set; }
        public int OrderCount { get; set; }

        public string HttpReferer { get; set; }

        public AdminNewUserModel()
        {
            // this prevents Automapper from blanking out default values in new User domain objects.
            OrderCount = 0;
            SignupDate = DateTime.Now;
        }
    }
}