using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Order
    // ** DAO Pattern

    public class OrderDao : IOrderDao
    {
        static OrderDao()
        {
            Mapper.CreateMap<OrderEntity, Order>();
            Mapper.CreateMap<Order, OrderEntity>();
        }

        public Order GetOrder(int orderId)
        {
            using (var context = new actionEntities())
            {
                var order = context.OrderEntities.SingleOrDefault(o => o.OrderId == orderId);
                return Mapper.Map<OrderEntity, Order>(order);
            }
        }

        public List<Order> GetOrdersByMember(int memberId)
        {
            using (var context = new actionEntities())
            {
                var orders = context.OrderEntities.Where(o => o.MemberId == memberId).ToList();
                return Mapper.Map<List<OrderEntity>, List<Order>>(orders);
            }
        }

        public List<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            using (var context = new actionEntities())
            {
                var orders = context.OrderEntities.Where(o => o.OrderDate >= dateFrom && o.OrderDate <= dateThru).ToList();
                return Mapper.Map<List<OrderEntity>, List<Order>>(orders);
            }
        }
    }
}
