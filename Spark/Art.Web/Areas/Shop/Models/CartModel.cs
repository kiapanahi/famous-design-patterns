using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Shop.Models
{
    public class CartModel
    {
        public IEnumerable<CartItemModel> Items =  Enumerable.Empty<CartItemModel>();
        
        public double GrandTotal { get; set; }

        public CartModel()
        {
            GrandTotal = 0;
        }
    }
}