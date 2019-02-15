using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Order Detail business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping


    public class OrderDetail : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern
        
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }

        // ** Enterprise Design Pattern: Foreign Key Mapping. Order is the parent

        public Order Order { get; set; }
    }
}
