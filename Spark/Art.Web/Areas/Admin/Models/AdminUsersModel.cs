using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class AdminUsersModel : PagedList<AdminUserModel>
    {
        public string Sort { get; set; }
        public IDictionary<string, string> SortItems { get; set; }
    }

    public class AdminUserModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OrderCount { get; set; }
        public DateTime? SignupDate { get; set; }
    }
}