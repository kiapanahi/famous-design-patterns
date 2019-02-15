using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    // defines methods to access orders.
    // this is a database-independent interface. Implementations are database specific
    // ** DAO Pattern
    
    public interface IOrderDao
    {
        // gets an specific order

        Order GetOrder(int orderId);

        // gets all orders for a customer

        List<Order> GetOrdersByMember(int memberId);

        // gets a list of orders placed within a date range

        List<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru);
    }
}
