using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.Views
{
    // represents view of orders
    public interface IOrdersView : IView
    {
        IList<OrderModel> Orders { set; }
    }
}
