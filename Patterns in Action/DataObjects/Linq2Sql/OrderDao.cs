using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using AutoMapper;
using System.Configuration;

namespace DataObjects.Linq2Sql
{
    // Data access object for Order
    // ** DAO Pattern

    public class OrderDao : IOrderDao
    {
        static OrderDao()
        {
            Mapper.CreateMap<Order, BusinessObjects.Order>();
        }

        public BusinessObjects.Order GetOrder(int orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var order = context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                return Mapper.Map<Order, BusinessObjects.Order>(order);
            }
        }

        public List<BusinessObjects.Order> GetOrdersByMember(int memberId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var orders = context.Orders.Where(o => o.MemberId == memberId).ToList();
                return Mapper.Map<List<Order>, List<BusinessObjects.Order>>(orders);
            }
        }

        public List<BusinessObjects.Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var orders = context.Orders.Where(o => o.OrderDate >= dateFrom && o.OrderDate <= dateThru).ToList();
                return Mapper.Map<List<Order>, List<BusinessObjects.Order>>(orders);
            }
        }
    }
}
