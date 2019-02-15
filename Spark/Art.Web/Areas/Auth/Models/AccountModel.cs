using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Auth.Models
{
    public class AccountModel
    {
        // account
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

        // password

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current Password is required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password is required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Min password length is 7 characters")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Confirm New Password is required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Min password length is 7 characters")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAuthenticatedWithOAuth { get; set; }
    }
}