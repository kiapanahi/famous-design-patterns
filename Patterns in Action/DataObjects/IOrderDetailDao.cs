using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    // defines methods to access order details.
    // this is a database-independent interface. Implementations are database specific
    // ** DAO Pattern
    
    public interface IOrderDetailDao
    {
    
        // gets a list of order details for a given order

        List<OrderDetail> GetOrderDetails(int orderId);
    }
}
