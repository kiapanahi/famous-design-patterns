using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Areas.Shop.Models
{
    
    // product ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Weight { get; set; }
        public string UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public string CategoryName { get; set; }
    }
}