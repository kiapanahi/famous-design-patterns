using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp.Models
{
    // Member business object as seen by the Service client.
    public class MemberModel
    {
        public MemberModel()
        {
            Orders = new List<OrderModel>();
        }
        public int MemberId{ get; set; }
        public string Email { get; set; }
        public string CompanyName{ get; set; }
        public string City{ get; set; }
        public string Country { get; set; }

        public IList<OrderModel> Orders { get; set; }
    }
}
