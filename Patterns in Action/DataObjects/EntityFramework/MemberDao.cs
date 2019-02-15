using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using BusinessObjects;

namespace DataObjects.EntityFramework
{
    // Data access object for Member
    // ** DAO Pattern

    public class MemberDao : IMemberDao
    {
        static MemberDao()
        {
            Mapper.CreateMap<MemberEntity, Member>();
            Mapper.CreateMap<Member, MemberEntity>();
        }
        public List<Member> GetMembers(string sortExpression = "MemberId ASC")
        {
            using (var context = new actionEntities())
            {
                var members = context.MemberEntities.AsQueryable().OrderBy(sortExpression).ToList();
                return Mapper.Map<List<MemberEntity>, List<Member>>(members);
            }
        }

        public Member GetMember(int memberId)
        {
            using (var context = new actionEntities())
            {
                var member = context.MemberEntities.FirstOrDefault(c => c.MemberId == memberId) as MemberEntity;
                return Mapper.Map<MemberEntity, Member>(member);
            }
        }

        public Member GetMemberByEmail(string email)
        {
            using (var context = new actionEntities())
            {
                var member = context.MemberEntities.FirstOrDefault(c => c.Email == email) as MemberEntity;
                return Mapper.Map<MemberEntity, Member>(member);
            }
        }

        public Member GetMemberByOrder(int orderId)
        {
            using (var context = new actionEntities())
            {
                var order = context.OrderEntities.Where(o => o.OrderId == orderId).SingleOrDefault() as OrderEntity;
                var member = context.MemberEntities.SingleOrDefault(c => c.MemberId == order.MemberId);

                return Mapper.Map<MemberEntity, Member>(member);
            }
        }

        public List<Member> GetMembersWithOrderStatistics(string sortExpression)
        {
            using (var context = new actionEntities())
            {
                // get members with orders

                var members = from m in context.MemberEntities
                              where context.OrderEntities.Any(o => o.MemberId == m.MemberId)
                              select m; 

                var orders = context.OrderEntities.OrderBy(o => o.MemberId);

                return members.AsQueryable().Select(m =>
                    new Member
                    {
                        MemberId = m.MemberId,
                        Email = m.Email,
                        CompanyName = m.CompanyName,
                        City = m.City,
                        Country = m.Country,
                        NumOrders = orders.Where(o => o.MemberId  == m.MemberId).Count(),
                        LastOrderDate = orders.Where(o => o.MemberId == m.MemberId).Max(o => o.OrderDate)
                    })
                    .OrderBy(sortExpression)
                    .ToList();
            }
        }

        public void InsertMember(Member member)
        {
            using (var context = new actionEntities())
            {
                var entity = Mapper.Map<Member, MemberEntity>(member);

                context.MemberEntities.Add(entity);
                context.SaveChanges();

                // update business object with new id
                member.MemberId = entity.MemberId;
            }
            
        }

        public void UpdateMember(Member member)
        {
            using (var context = new actionEntities())
            {
                var entity = context.MemberEntities.SingleOrDefault(m => m.MemberId == member.MemberId);
                entity.Email = member.Email;
                entity.CompanyName = member.CompanyName;
                entity.Country = member.Country;
                entity.City = member.City;

                //context.Members.Attach(entity); 
                context.SaveChanges();
             
            }
        }

        public void DeleteMember(Member member)
        {
            using (var context = new actionEntities())
            {
                var entity = context.MemberEntities.SingleOrDefault(m => m.MemberId == member.MemberId);
                context.MemberEntities.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
