using ActionService;
using AutoMapper;
using BusinessObjects;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WinFormsApp.Models
{
    /// <summary>
    /// The Model in MVP design pattern. 
    /// Implements IModel and communicates with WCF Service.
    /// </summary>
    public class Model : IModel
    {
        static Service service = new Service();

        static Model()
        {
            Mapper.CreateMap<Member, MemberModel>();
            Mapper.CreateMap<MemberModel, Member>();
            Mapper.CreateMap<Order, OrderModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailModel>();
        }

        #region Login / Logout

        // Logs in to the service.
        public bool Login(string email, string password)
        {
            // beneath Service runs WebService which unfortunately was not really designed for Windows apps. 
            // one solution would be to use a webservice

            // return service.Login(email, password);
            return true;

        }

        // Logs out of the service.
        public void Logout()
        {
            // beneath Service runs WebService which unfortunately was not really designed for Windows apps. 
            // one solution would be to use a webservice

            // service.Logout();
        }

        #endregion

        #region Members

        // gets a complete list of Members and their orders and order details.

        public List<MemberModel> GetMembers(string sortExpression)
        {
            var members = service.GetMembers(sortExpression);
            return Mapper.Map<List<Member>, List<MemberModel>>(members);
        }

        // gets a specific Member.
        public MemberModel GetMember(int memberId)
        {
            var member = service.GetMember(memberId);
            return Mapper.Map<Member, MemberModel>(member);
        }

        #endregion

        #region Member persistence

        // adds a new Member to the database.
        public void AddMember(MemberModel model)
        {
            var member = Mapper.Map<MemberModel, Member>(model);
            service.InsertMember(member);
        }

        // updates an existing Member in the database.
        public void UpdateMember(MemberModel model)
        {
            var member = Mapper.Map<MemberModel, Member>(model);
            service.UpdateMember(member);
        }

        // geletes a Member record.
        public void DeleteMember(int memberId)
        {
            var member = service.GetMember(memberId);
            service.DeleteMember(member);
        }

        #endregion

        #region Orders

        // gets a list of orders for a given Member.

        public List<OrderModel> GetOrders(int memberId)
        {
            var orders = service.GetOrdersByMember(memberId);
            var models = Mapper.Map<List<Order>, List<OrderModel>>(orders);

            // get all products
            var products = service.SearchProducts("", 0, 5000, "ProductId ASC").ToDictionary(p => p.ProductId);

            // rather inefficient. the service API is not flexible enough to perform larger batch retrieves.
            // see Spark for a richer API with generic Repositories
            foreach (var model in models)
            {
                var details = service.GetOrderDetails(model.OrderId);
                details.ForEach(d => d.ProductName = products[d.ProductId].ProductName);
                model.OrderDetails = Mapper.Map<List<OrderDetail>, List<OrderDetailModel>>(details);
            }

            return models;
        }

        #endregion
    }
}
