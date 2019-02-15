using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using AutoMapper;
using System.Configuration;

namespace DataObjects.Linq2Sql
{
    // Data access object for Member
    // ** DAO Pattern

    public class MemberDao : IMemberDao
    {
        static MemberDao()
        {
            Mapper.CreateMap<Member, BusinessObjects.Member>();
            Mapper.CreateMap<BusinessObjects.Member, Member>();
        }

        public BusinessObjects.Member GetMember(int memberId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var member = context.Members.SingleOrDefault(m => m.MemberId == memberId);
                return Mapper.Map<Member, BusinessObjects.Member>(member);
            }
        }

        public BusinessObjects.Member GetMemberByEmail(string email)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var member = context.Members.SingleOrDefault(m => m.Email == email);
                return Mapper.Map<Member, BusinessObjects.Member>(member);
            }
        }

        public List<BusinessObjects.Member> GetMembers(string sortExpression = "MemberId ASC")
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var members = context.Members.OrderBy(sortExpression).ToList();
                return Mapper.Map<List<Member>, List<BusinessObjects.Member>>(members);
            }
        }

        public BusinessObjects.Member GetMemberByOrder(int orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var member = context.Members.SelectMany(c => context.Orders
                        .Where(o => c.MemberId == o.MemberId && o.OrderId == orderId),
                         (c, o) => c).SingleOrDefault(c => true);

                return Mapper.Map<Member, BusinessObjects.Member>(member);
            }
        }

        public List<BusinessObjects.Member> GetMembersWithOrderStatistics(string sortExpression)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var customers = context.Members.Select(c =>
                    new BusinessObjects.Member
                    {
                        MemberId = c.MemberId,
                        Email = c.Email,
                        CompanyName = c.CompanyName,
                        City = c.City,
                        Country = c.Country,
                        NumOrders = context.Orders.Where(o => o.MemberId == c.MemberId).Count(),
                        LastOrderDate = context.Orders.Where(o => o.MemberId == c.MemberId).Max(o => o.OrderDate)
                    }).OrderBy(sortExpression); 

                // Exclude customers without orders
                return customers.Where(c => c.NumOrders > 0).ToList();
            }
        }

        public void InsertMember(BusinessObjects.Member member)
        {
            var entity = Mapper.Map<BusinessObjects.Member, Member>(member);

            using (var context = DataContextFactory.CreateContext())
            {
                context.Members.InsertOnSubmit(entity);
                context.SubmitChanges();

                // update business object with new id
                member.MemberId = entity.MemberId;
            }
        }

        public void UpdateMember(BusinessObjects.Member member)
        {
            var entity = Mapper.Map<BusinessObjects.Member, Member>(member);

            using (var context = DataContextFactory.CreateContext())
            {
                var original = context.Members.SingleOrDefault(m => m.MemberId == member.MemberId);

                original.Email = member.Email;
                original.CompanyName = member.CompanyName;
                original.Country = member.Country;
                original.City = member.City;
                
                context.SubmitChanges();
            }
        }

        public void DeleteMember(BusinessObjects.Member member)
        {
            var entity = Mapper.Map<BusinessObjects.Member, Member>(member);

            using (var context = DataContextFactory.CreateContext())
            {
                context.Members.Attach(entity, false);
                context.Members.DeleteOnSubmit(entity);
                context.SubmitChanges();
            }
        }
    }
}
