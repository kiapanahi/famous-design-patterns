using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class Order : Entity<Order>
    {
        protected override void OnInserting(ref string sql)
        {
            sql += "; UPDATE [User] Set OrderCount = OrderCount + 1 WHERE Id = " + this.UserId + ";";
        }

        protected override void OnDeleting(ref string sql)
        {
            sql += "; UPDATE [User] Set OrderCount = OrderCount - 1 WHERE Id = " + this.UserId + ";";
        }
	} 
}	
