using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    // Category business object
    // ** Enterprise Design Pattern: Domain Model, Identity Field


    public class Category : BusinessObject
    {
        // ** Enterprise Design Pattern: Identity field pattern

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
