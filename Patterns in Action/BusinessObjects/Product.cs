using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
    // Product business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field, Foreign key mapping

    public class Product : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string Weight { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        // ** Enterprise Design Pattern: Foreign Key Mapping. Category is the parent

        public Category Category { get; set; }
    }
}

