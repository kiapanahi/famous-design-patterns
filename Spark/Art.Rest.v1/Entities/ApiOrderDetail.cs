using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiOrderDetail : ApiEntity
	{ 
        public ApiEntity Order { get; set; }
        public ApiEntity Product { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
	} 
}	
