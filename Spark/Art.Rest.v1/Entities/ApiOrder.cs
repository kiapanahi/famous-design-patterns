using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiOrder : ApiEntity
	{ 
        public ApiOrder()
        {
            OrderDetails = new List<ApiEntity>();
        }
        public ApiEntity User { get; set; }
        public DateTime? OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public int OrderNumber { get; set; }
        public int ItemCount { get; set; }
        public List<ApiEntity> OrderDetails { get; set; }
	} 
}	
