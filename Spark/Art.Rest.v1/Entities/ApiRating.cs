using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiRating : ApiEntity
	{ 
        public ApiEntity User { get; set; }
        public ApiEntity Product { get; set; }
        public int Stars { get; set; }
	} 
}	
