using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mvc.Code;

namespace Mvc.Areas.Admin.Models
{
    // Member ViewModel 
    // ** Data Transfer Object (DTO) pattern

    public class MemberModel
    {
        [DisplayName("Id")]
        public int MemberId { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email can be at most 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(30, ErrorMessage = "Company Name can be at most 30 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(30, ErrorMessage = "City can be at most 30 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(30, ErrorMessage = "Country can be at most 30 characters")]
        public string Country { get; set; }

        public int NumOrders { get; set; }
        public string LastOrderDate { get; set; }

        // unique photo identifier

        public int PhotoId { get; set; }
    }
}