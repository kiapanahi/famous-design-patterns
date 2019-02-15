using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    
    // defines methods to access Members.
    // this is a database-independent interface. Implementations are database specific
    // ** DAO Pattern

    
    public interface IMemberDao
    {
        // gets a specific Member

        Member GetMember(int memberId);

        // gets a specific Member by email

        Member GetMemberByEmail(string email);

        // gets a sorted list of all Members

        List<Member> GetMembers(string sortExpression = "MemberId ASC");

        // gets Member given an order

        Member GetMemberByOrder(int orderId);

        // gets Members with order statistics in given sort order.

        List<Member> GetMembersWithOrderStatistics(string sortExpression);

        // inserts a new Member
        // following insert, Member object will contain the new identifier
        
        void InsertMember(Member member);

        // updates a Member

        void UpdateMember(Member member);

        // deletes a Member

        void DeleteMember(Member member);
    }
}
