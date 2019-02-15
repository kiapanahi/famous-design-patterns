using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiError : ApiEntity
	{ 
        public ApiEntity User { get; set; }
        public DateTime? ErrorDate { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string Everything { get; set; }
        public string HttpReferer { get; set; }
        public string PathAndQuery { get; set; }
	} 
}	
