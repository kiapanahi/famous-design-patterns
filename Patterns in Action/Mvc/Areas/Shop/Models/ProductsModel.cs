using Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Areas.Shop.Models
{
    // list of products ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class ProductsModel
    {
        public SelectList Categories { get; set; }

        // sortable list of products

        public SortedList<ProductModel> Products { get; set; }
    }
}