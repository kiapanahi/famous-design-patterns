using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientArtist 
	{
        public ClientArtist()
        {
            Products = new List<ClientProduct>();
        }
        public string Href { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LifeSpan { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        public List<ClientProduct> Products { get; set; }
	} 
}	
