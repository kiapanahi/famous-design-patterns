using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Plan.Domain;

namespace Plan.Rest.v1
{
    // Generated 07/26/2013 00:04:12
	
    public class ApiTask : ApiEntity
	{ 
        public ApiEntity User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
	} 
}	
