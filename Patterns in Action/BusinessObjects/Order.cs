using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
    
    public class Order : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public double Freight { get; set; }

        // ** Enterprise Design Pattern: Foreign Key Mapping. Member is the parent

        public Member Member { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
