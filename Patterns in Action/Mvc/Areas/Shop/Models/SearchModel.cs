using Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Areas.Shop.Models
{
    
    // search page ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class SearchModel
    {
        public string ProductName { get; set; }
        public SelectList PriceRanges { get; set; }

        // sortable list of products

        public SortedList<ProductModel> Products { get; set; }
    }
}