using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class CartItem : Entity<CartItem>
    {
        protected override void OnInserting(ref string sql)
        {
            sql += "; UPDATE [Cart] Set ItemCount = ItemCount + 1 WHERE Id = " + this.CartId + ";";
        }

        protected override void OnDeleting(ref string sql)
        {
            sql += "; UPDATE [Cart] Set ItemCount = ItemCount - 1 WHERE Id = " + this.CartId + ";";
        }
	} 
}	
