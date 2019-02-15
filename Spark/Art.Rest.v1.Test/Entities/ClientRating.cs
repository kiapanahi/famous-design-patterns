using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Rest.v1.Test
{
    public class ClientRating
	{
        public string Href { get; set; }
        public ClientUser User { get; set; }
        public ClientProduct Product { get; set; }
        public int Stars { get; set; }
	} 
}	
