using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfApp.Models
{
    
    // model of the Order. 
    // not inherited from BaseModel because no orders are placed in this app
    
    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public float Freight { get; set; }

        public MemberModel Member { get; set; }
        public ObservableCollection<OrderDetailModel> OrderDetails { get; set; }
    }
}
