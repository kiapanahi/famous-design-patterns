using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsApp.Views;

namespace WinFormsApp.Presenters
{
    /// <summary>
    /// Login Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class LoginPresenter : Presenter<ILoginView>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="view">The view</param>
        public LoginPresenter(ILoginView view)
            : base(view)
        {
        }

        /// <summary>
        /// Perform login. Gets data from view and calls model.
        /// </summary>
        public void Login()
        {
            string email = View.Email;
            string password = View.Password;

            Model.Login(email, password);
        }
    }
}
