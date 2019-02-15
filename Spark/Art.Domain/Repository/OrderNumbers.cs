using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class OrderNumbers : Repository<OrderNumber> 
	{
        public int Next()
        {
            // default is autocommit, so this is a single transaction

            var on = Query("UPDATE [OrderNumber] SET Number = Number + 1; SELECT Number FROM [OrderNumber];").ToList();
            return on[0].Number;
        }
	}
}	
