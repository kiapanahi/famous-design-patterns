using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiCart : ApiEntity
	{ 
        public ApiCart()
        {
            CartItems = new List<ApiEntity>();
        }
        public string Cookie { get; set; }
        public DateTime? CartDate { get; set; }
        public int ItemCount { get; set; }
        public List<ApiEntity> CartItems { get; set; }
	} 
}	
