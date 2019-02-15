using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsApp.Models
{
    // IModel interface, part of MVP design pattern. 

    public interface IModel
    {
        bool Login(string userName, string password);
        void Logout();

        List<MemberModel> GetMembers(string sortExpression);
        MemberModel GetMember(int memberId);

        void AddMember(MemberModel member);
        void UpdateMember(MemberModel member);
        void DeleteMember(int memberId);

        List<OrderModel> GetOrders(int memberId);
    }
}
