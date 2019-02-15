using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Order Detail
    // ** DAO Pattern

    public class OrderDetailDao : IOrderDetailDao
    {
        static OrderDetailDao()
        {
            Mapper.CreateMap<OrderDetailEntity, OrderDetail>();
        }

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            using (var context = new actionEntities())
            {
                var orderDetails = context.OrderDetailEntities.Where(d => d.OrderId == orderId);

                int[] keys = orderDetails.Select(d=>d.ProductId).ToArray();
                var products = 
                    (from p in context.ProductEntities
                    where keys.Contains(p.ProductId)
                    select p).ToDictionary(p=> p.ProductId);

                var details = Mapper.Map<IEnumerable<OrderDetailEntity>, List<OrderDetail>>(orderDetails);
                details.ForEach(d => d.ProductName = products[d.ProductId].ProductName);

                return details;
            }
        }
    }
}
