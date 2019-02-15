using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Shop.Models
{
    public class CartItemModel
    {
        public int? Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int? ProductId { get; set; }

        public double SubTotal { get; set; }
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string ProductAvgStars { get; set; }
        public string ProductArtistName { get; set; }
    }
}