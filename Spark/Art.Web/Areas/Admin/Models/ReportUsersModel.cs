using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Art.Web.Areas.Admin.Models
{
    public class ReportUsersModel : PagedList<ReportUserModel>
    {
        public string Sort { get; set; }
        public IDictionary<string, string> SortItems { get; set; }
        public bool OrdersOnly { get; set; }
    }

    public class ReportUserModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int OrderCount { get; set; }
    }
}