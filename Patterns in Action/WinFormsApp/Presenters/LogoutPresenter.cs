using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WinFormsApp.Views;

namespace WinFormsApp.Presenters
{
    /// <summary>
    /// Logout Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class LogoutPresenter : Presenter<IView>
    {
        /// <summary>
        /// Constructor. View is not really used here.
        /// </summary>
        /// <param name="view">The view.</param>
        public LogoutPresenter(IView view)
            : base(view)
        {
        }

        /// <summary>
        /// Informs model to logout.
        /// </summary>
        public void Logout()
        {
            Model.Logout();
        }
    }
}
