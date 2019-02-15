using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApp.Models
{
    
    // model of order details
    // not inherited from BaseModel because no orders are placed in this app
    
    public class OrderDetailModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Discount { get; set; }

        public OrderModel Order { get; set; }
    }
}
