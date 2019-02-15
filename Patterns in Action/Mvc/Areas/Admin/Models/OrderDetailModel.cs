using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Areas.Admin.Models
{
    // order detail ViewModel

    // ** Data Transfer Object (DTO) pattern


    public class OrderDetailModel
    {
        public int OrderDetailId;
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string Total { get; set; }
        public string Discount { get; set; }
    }
}