using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiUser : ApiEntity
	{ 
        public ApiUser()
        {
            Orders = new List<ApiEntity>();
            Ratings = new List<ApiEntity>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? SignupDate { get; set; }
        public int OrderCount { get; set; }
        public List<ApiEntity> Orders { get; set; }
        public List<ApiEntity> Ratings { get; set; }
	} 
}	
