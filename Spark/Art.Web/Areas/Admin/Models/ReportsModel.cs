using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class ReportsModel
    {
        public double? TotalRevenues { get; set; }
        public double? LastWeekRevenues { get; set; }

        public int UserCount { get; set; }
        public int OrderCount { get; set; }
        public int ProductCount { get; set; }
    }
}