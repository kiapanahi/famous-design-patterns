using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientCart 
	{ 
        public ClientCart()
        {
            CartItems = new List<ClientCartItem>();
        }
        public string Href { get; set; }
        public string Cookie { get; set; }
        public DateTime? CartDate { get; set; }
        public int ItemCount { get; set; }
        public List<ClientCartItem> CartItems { get; set; }
	} 
}	
