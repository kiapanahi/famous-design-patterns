using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{
    // Data access object for Member
    // ** DAO Pattern

    public class MemberDao : IMemberDao
    {
        static Db db = new Db();

        public Member GetMember(int MemberId)
        {
            string sql =
            @" SELECT MemberId, Email, CompanyName, City, Country
                 FROM [Member]
                WHERE MemberId = @MemberId";

            object[] parms = { "@MemberId", MemberId };
            return db.Read(sql, Make, parms).FirstOrDefault();
        }

        public Member GetMemberByEmail(string email)
        {
            string sql =
            @" SELECT MemberId, Email, CompanyName, City, Country
                 FROM [Member]
                WHERE Email = @Email";

            object[] parms = { "@Email", email };
            return db.Read(sql, Make, parms).FirstOrDefault();
        }

        public List<Member> GetMembers(string sortExpression)
        {
            string sql =
            @"SELECT MemberId, Email, CompanyName, City, Country
                FROM [Member] ".OrderBy(sortExpression);

            return db.Read(sql, Make).ToList();
        }

        public Member GetMemberByOrder(int orderId)
        {
            string sql =
            @" SELECT C.MemberId, Email, CompanyName, City, Country
                 FROM [Order] O JOIN [Member] C ON O.MemberId = C.MemberId
                WHERE OrderId = @OrderId";

            object[] parms = { "@OrderId", orderId };
            return db.Read(sql, Make, parms).FirstOrDefault();
        }

        public List<Member> GetMembersWithOrderStatistics(string sortExpression)
        {
            string sql =
            @"SELECT C.MemberId, Email, CompanyName, City, Country,
                     MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders 
                FROM [Order] O JOIN [Member] C ON O.MemberId = C.MemberId
               GROUP BY C.MemberId, Email, CompanyName, City, Country "
                    .OrderBy(sortExpression);

            return db.Read(sql, MakeWithStats).ToList();
        }

        public void InsertMember(Member Member)
        {
            string sql =
            @"INSERT INTO [Member] (Email, CompanyName, City, Country) 			
              VALUES (@Email, @CompanyName, @City, @Country)";

            Member.MemberId = db.Insert(sql, Take(Member));
            
        }
       
        public void UpdateMember(Member Member)
        {
            string sql =
            @"UPDATE [Member]
                 SET Email = @Email, 
                     CompanyName = @CompanyName,
                     City = @City,
                     Country = @Country
               WHERE MemberId = @MemberId";
                 

            db.Update(sql, Take(Member));
        }

        public void DeleteMember(Member Member)
        {
            string sql =
            @"DELETE FROM [Member]
               WHERE MemberId = @MemberId";

            db.Update(sql, Take(Member));
        }

        
        // creates a Member object based on DataReader

        static Func<IDataReader, Member> Make = reader =>
           new Member
           {
               MemberId = reader["MemberId"].AsId(),
               Email = reader["Email"].AsString(),
               CompanyName = reader["CompanyName"].AsString(),
               City = reader["City"].AsString(),
               Country = reader["Country"].AsString()
           };

        // creates a Members object with order statistics based on DataReader
        
        static Func<IDataReader, Member> MakeWithStats = reader =>
        {
            var member = Make(reader);
            member.NumOrders = reader["NumOrders"].AsInt();
            member.LastOrderDate = reader["LastOrderDate"].AsDateTime();

            return member;
        };

        
        // creates query parameters list from Member object

        object[] Take(Member Member)
        {
            return new object[]  
            {
                "@MemberId", Member.MemberId,
                "@Email", Member.Email,
                "@CompanyName", Member.CompanyName,
                "@City", Member.City,
                "@Country", Member.Country
            };
        }
    }
}
