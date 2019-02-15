using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsApp.Views;


namespace WinFormsApp.Presenters
{
    /// <summary>
    /// Orders Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class OrdersPresenter : Presenter<IOrdersView>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">The view</param>
        public OrdersPresenter(IOrdersView view)
            : base(view)
        {
        }

        /// <summary>
        /// Displays list of orders.
        /// </summary>
        /// <param name="memberId">Member id to display.</param>
        public void Display(int memberId)
        {
            View.Orders = Model.GetOrders(memberId); 
        }
    }
}
