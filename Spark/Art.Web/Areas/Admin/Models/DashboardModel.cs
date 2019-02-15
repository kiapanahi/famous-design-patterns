using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class DashboardModel
    {
        public string UsersData { get; set; }
        public string UsersTicks { get; set; }
        public string SalesData { get; set; }
        public string SalesTicks { get; set; }

        public string Demographics { get; set; }
    }
}