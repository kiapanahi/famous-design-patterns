using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Art.Domain;

namespace Art.Rest.v1
{
    // Generated 07/20/2013 16:40:13

    public class ApiProduct : ApiEntity
	{ 
        public ApiProduct()
        {
            OrderDetails = new List<ApiEntity>();
            Ratings = new List<ApiEntity>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public ApiEntity Artist { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int QuantitySold { get; set; }
        public double AvgStars { get; set; }
        public List<ApiEntity> OrderDetails { get; set; }
        public List<ApiEntity> Ratings { get; set; }
	} 
}	
