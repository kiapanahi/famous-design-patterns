using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Art.Web.Areas.Auth.Models
{
    public class LoginModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required (ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage="Min password length is 5 characters")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}