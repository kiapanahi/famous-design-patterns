using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Areas.Admin.Models
{
    // list of orders for member ViewModel

    // ** Data Transfer Object (DTO) pattern

    public class MemberOrdersModel
    {
        public MemberModel Member { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}