using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class ReportOrdersModel : PagedList<ReportOrderModel>
    {
        public string Sort { get; set; }
        public IDictionary<string, string> SortItems { get; set; }
    }

    public class ReportOrderModel
    {
        public int? Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public int ItemCount { get; set; }

        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}