using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionService
{
    // single interface to all 'repositories'

    public interface IService
    {
        // Category Repository

        List<Category> GetCategories();
        Category GetCategoryByProduct(int productId);

        // Product Repository

        Product GetProduct(int productId);
        List<Product> GetProductsByCategory(int categoryId, string sortExpression);
        List<Product> SearchProducts(string productName, double priceFrom, double priceThru, string sortExpression);

        // Member Repository

        Member GetMember(int memberId);
        Member GetMemberByEmail(string email);
        List<Member> GetMembers(string sortExpression);
        Member GetMemberByOrder(int orderId);
        List<Member> GetMembersWithOrderStatistics(string sortExpression);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);

        // Order Repository

        Order GetOrder(int orderId);
        List<Order> GetOrdersByMember(int memberId);
        List<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru);

        // OrderDetail Repository

        List<OrderDetail> GetOrderDetails(int orderId);
        bool Login(string email, string password);
        void Logout();
    }
}
