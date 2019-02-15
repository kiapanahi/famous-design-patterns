using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class ReportOrderDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int OrderNumber { get; set; }

        public List<ReportOrderDetailModel> OrderDetails { get; set; }
        
        public string HttpReferer { get; set; }

        public ReportOrderDetailsModel()
        {
            OrderDetails = new List<ReportOrderDetailModel>();
        }
    }

    public class ReportOrderDetailModel
    {
        public int? ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public string ProductArtist { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}