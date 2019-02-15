using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13
	
    public class ApiArtist : ApiEntity
	{ 
        public ApiArtist()
        {
            Products = new List<ApiEntity>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LifeSpan { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        public List<ApiEntity> Products { get; set; }
	} 
}	
