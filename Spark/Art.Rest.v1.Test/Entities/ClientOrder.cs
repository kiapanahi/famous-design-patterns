using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientOrder 
	{
        public ClientOrder()
        {
            OrderDetails = new List<ClientOrderDetail>();
        }
        public string Href { get; set; }
        public ClientUser User { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public List<ClientOrderDetail> OrderDetails { get; set; }
	} 
}	
