using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Areas.Admin.Models
{
    // order ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class OrderModel
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string Freight { get; set; }
    }
}