using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{
    // Data access object for Order
    // ** DAO Pattern

    public class OrderDao : IOrderDao
    {
        static Db db = new Db();
       
        public Order GetOrder(int orderId)
        {
            
            string sql =
            @"SELECT OrderId, OrderDate, RequiredDate, Freight
              FROM [Order] 
             WHERE OrderId = @OrderId";

            object[] parms = { "@OrderId", orderId };
            return db.Read(sql, Make, parms).FirstOrDefault();
        }

        public List<Order> GetOrdersByMember(int memberId)
        {
            string sql =
              @" SELECT OrderId, OrderDate, RequiredDate, Freight
                   FROM [Order]
                  WHERE MemberId = @MemberId
               ORDER BY OrderDate ASC";

            object[] parms = { "@MemberId", memberId };
            return db.Read(sql, Make, parms).ToList();
        }

        public List<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            string sql =
            @" SELECT OrderId, OrderDate, RequiredDate, Freight
                 FROM [Order]
                WHERE OrderDate >= @DateFrom
                  AND OrderDate <= @DateThru
                ORDER BY OrderDate ASC ";

            object[] parms = { "@DateFrom", dateFrom, "@DateThru", dateThru };
            return db.Read(sql, Make, parms).ToList();
        }

        
        // creates an Order object based on DataReader.
        
        static Func<IDataReader, Order> Make = reader =>
           new Order
           {
               OrderId = reader["OrderId"].AsId(),
               OrderDate = reader["OrderDate"].AsDateTime(),
               RequiredDate = reader["RequiredDate"].AsDateTime(),
               Freight = reader["Freight"].AsDouble()
           };

        
        // creates query parameters list from Order object

        object[] Take(Order order)
        {
            return new object[]  
            {
                "@OrderId", order.OrderId,
                "@OrderDate", order.OrderDate,
                "@RequiredDate", order.RequiredDate,
                "@Freight", order.Freight
            };
        }
    }
}
