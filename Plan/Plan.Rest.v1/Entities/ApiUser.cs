using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Plan.Domain;

namespace Plan.Rest.v1
{
    // Generated 07/26/2013 00:04:12
	
    public class ApiUser : ApiEntity
	{ 
        public ApiUser()
        {
            Tasks = new List<ApiEntity>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<ApiEntity> Tasks { get; set; }
	} 
}	
