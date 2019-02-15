using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class ReportProductsModel : PagedList<ReportProductModel>
    {
        public string Sort { get; set; }
        public IDictionary<string, string> SortItems { get; set; }
    }

    public class ReportProductModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double AvgStars { get; set; }
        public int QuantitySold { get; set; }
        public double Revenue { get; set; }

        public int? ArtistId { get; set; }
        public string ArtistName { get; set; }

    }
}