using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Linq2Sql
{
    // Data access object for OrderDetail
    // ** DAO Pattern

    public class OrderDetailDao : IOrderDetailDao
    {
        static OrderDetailDao()
        {
            Mapper.CreateMap<OrderDetail, BusinessObjects.OrderDetail>();
        }

        public List<BusinessObjects.OrderDetail> GetOrderDetails(int orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var orderDetails = context.OrderDetails.Where(d => d.OrderId == orderId).ToList();
                return Mapper.Map<List<OrderDetail>, List<BusinessObjects.OrderDetail>>(orderDetails);
            }
        }
    }
}
