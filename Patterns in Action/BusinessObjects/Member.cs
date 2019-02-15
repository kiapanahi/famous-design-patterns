using BusinessObjects.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    // Member business object

    // ** Enterprise Design Pattern: Domain Model, Identity Field.
    // this is also the place where business rules are established.

    public class Member : BusinessObject
    {
        public Member()
        {
            // establish business rules

            AddRule(new ValidateId("MemberId"));

            AddRule(new ValidateRequired("Email"));
            AddRule(new ValidateLength("Email", 1, 100));
            AddRule(new ValidateEmail("Email"));

            AddRule(new ValidateRequired("CompanyName"));
            AddRule(new ValidateLength("CompanyName", 1, 40));

            AddRule(new ValidateRequired("City"));
            AddRule(new ValidateLength("City", 1, 15));

            AddRule(new ValidateRequired("Country"));
            AddRule(new ValidateLength("Country", 1, 15));
        }

        // ** Enterprise Design Pattern: Identity field pattern

        public int MemberId { get; set; }

        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int NumOrders { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}
