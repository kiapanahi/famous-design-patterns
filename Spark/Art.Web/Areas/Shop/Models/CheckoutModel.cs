using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Shop.Models
{
    public class CheckoutModel
    {
        public int ItemCount { get; set; }
        public double GrandTotal { get; set; }
        
        public string CreditcardNumber { get; set; }
        public string CreditcardType { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
    }
}