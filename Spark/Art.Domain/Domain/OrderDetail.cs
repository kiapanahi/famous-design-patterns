using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace Art.Domain
{
    // Generated 07/24/2013 16:13:17

    // Add custom code inside partial class

    public partial class OrderDetail : Entity<OrderDetail> 
	{
        protected override void OnInserting(ref string sql)
        {
            sql += "; UPDATE [Product] Set QuantitySold = QuantitySold + " + Quantity + " WHERE Id = " + ProductId;
            sql += "; UPDATE [Order] Set ItemCount = ItemCount + 1 WHERE Id = " + OrderId + ";";
        }
        protected override void OnUpdating(ref string sql)
        {
            // recalculate quantity sold
            sql += "; UPDATE [Product] Set QuantitySold = (SELECT SUM(D.Quantity) FROM [OrderDetail] D WHERE D.ProductId = " + ProductId + ") WHERE Id = " + ProductId + ";";
        }
        protected override void OnDeleting(ref string sql)
        {
            sql += "; UPDATE [Product] Set QuantitySold = QuantitySold - " + Quantity + " WHERE Id = " + ProductId;
            sql += "; UPDATE [Order] Set ItemCount = ItemCount - 1 WHERE Id = " + OrderId + ";";
        }
	} 
}	
