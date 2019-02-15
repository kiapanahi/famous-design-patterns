using ActionService;
using AutoMapper;
using BusinessObjects;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace WpfApp.Models
{
    
    // implementation of provider interface to Services.

    public class Provider : IProvider
    {
        static Service service = new Service();

        // establishes Automapper object mappings

        public Provider()
        {
            Mapper.CreateMap<Member, MemberModel>()
                .ConstructUsing((Func<Member, MemberModel>)(m => new MemberModel(this)));

            Mapper.CreateMap<MemberModel, Member>();
            Mapper.CreateMap<Order, OrderModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailModel>();
        }

        #region Login / Logout

        public bool Login(string email, string password)
        {
            // commented out because SimpleMembership is not designed for the web
            // return service.Login(email, password)
            return true;
        }

        public void Logout()
        {
            // commented out because SimpleMembership is not designed for the web
            // service.Logout
        }

        #endregion

        #region Members 

        
        // gets an observable collection of all members in the given sort order

        public ObservableCollection<MemberModel> GetMembers(string sortExpression)
        {
            var members = service.GetMembers(sortExpression);
            return Mapper.Map<List<Member>, ObservableCollection<MemberModel>>(members);
        }

        public MemberModel GetMember(int memberId)
        {
            var member = service.GetMember(memberId);
            return Mapper.Map<Member, MemberModel>(member);
           
        }

        #endregion

        #region Member persistence

        // adds a new member to the database

        public void AddMember(MemberModel model)
        {
            var member = Mapper.Map<MemberModel, Member>(model);
            service.InsertMember(member);
            
            // retrieve new memberid
            model.MemberId = member.MemberId;
        }

        // updates an existing member in the database
        
        public void UpdateMember(MemberModel model)
        {
            var member = Mapper.Map<MemberModel, Member>(model);
            service.UpdateMember(member);
            
        }

        //  deletes a member record

        public void DeleteMember(int memberId)
        {
            var member = service.GetMember(memberId);
            service.DeleteMember(member);
        }

        #endregion

        #region Orders

        
        // gets an observable collection of orders for a given member
        
        public ObservableCollection<OrderModel> GetOrders(int memberId)
        {
            var orders = service.GetOrdersByMember(memberId);
            var models = Mapper.Map<List<Order>, ObservableCollection<OrderModel>>(orders);

            // get all products

            var products = service.SearchProducts("", 0, 5000, "ProductId ASC").ToDictionary(p => p.ProductId);

            // rather inefficient. the service API is not flexible enough to perform larger batch retrieves.
            // see Spark for a richer API with generic Repositories

            foreach (var model in models)
            {
                var details = service.GetOrderDetails(model.OrderId);
                details.ForEach(d => d.ProductName = products[d.ProductId].ProductName);
                model.OrderDetails = Mapper.Map<List<OrderDetail>, ObservableCollection<OrderDetailModel>>(details);
            }

            return models;
        }

        #endregion
    }
}
