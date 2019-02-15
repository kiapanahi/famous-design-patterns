using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp.Models
{
    
    // Order business object as seen by the Service client.
    
    public class OrderModel
    {
        public int OrderId{ get; set; }
        public DateTime OrderDate{ get; set; }
        public DateTime RequiredDate{ get; set; }
        public float Freight{ get; set; }

        public IList<OrderDetailModel> OrderDetails { get; set; }
        public MemberModel Member{ get; set; }
    }
}
