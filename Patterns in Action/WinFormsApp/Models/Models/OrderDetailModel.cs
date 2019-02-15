using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp.Models
{
    
    // Order detail business object as defined on service client side

    public class OrderDetailModel
    {
        public string ProductName{ get; set; }
        public int Quantity{ get; set; }
        public float UnitPrice{ get; set; }
        public float Discount{ get; set; }

        public OrderModel Order{ get; set; }
    }
}
