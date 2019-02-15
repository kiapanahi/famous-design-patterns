using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientUser
	{
        public ClientUser()
        {
            Orders = new List<ClientOrder>();
        }
        public string Href { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int OrderCount { get; set; }
        public List<ClientOrder> Orders { get; set; }
	} 
}	
