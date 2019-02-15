using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{

    // Data access object for OrderDetail
    // ** DAO Pattern

    public class OrderDetailDao : IOrderDetailDao
    {
        static Db db = new Db();

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            string sql =
            @"SELECT OrderId, O.ProductId, ProductName, O.UnitPrice, Quantity, Discount
                FROM OrderDetail O JOIN Product P ON O.ProductId = P.ProductId 
               WHERE OrderId = @OrderId";

            object[] parms = { "@OrderId", orderId };
            return db.Read(sql, Make, parms).ToList();
        }

        
        // creates order detail object from IDataReader
        
        static Func<IDataReader, OrderDetail> Make = reader =>
          new OrderDetail
          {
              OrderId = reader["OrderId"].AsId(),
              ProductId = reader["ProductId"].AsId(),
              ProductName = reader["ProductName"].AsString(),
              Quantity = reader["Quantity"].AsInt(),
              UnitPrice = reader["UnitPrice"].AsDouble(),
              Discount = reader["Discount"].AsDouble()
          };

        
        // creates query parameter list from order detail object
        
        object[] Take(OrderDetail orderDetail)
        {
            return new object[]  
            {
                "@OrderId", orderDetail.OrderId,
                "@ProductId", orderDetail.ProductId,
                "@ProductName", orderDetail.ProductName,
                "@Quantity", orderDetail.Quantity,
                "@UnitPrice", orderDetail.UnitPrice,
                "@Discount", orderDetail.Discount
            };
        }
    }
}
