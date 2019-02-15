using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientProduct 
	{
        public ClientProduct()
        {
            OrderDetails = new List<ClientOrderDetail>();
        }
        public string Href { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ClientArtist Artist { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double AvgStars { get; set; }
        public int QuantitySold { get; set; }
        public List<ClientOrderDetail> OrderDetails { get; set; }
	} 
}	
