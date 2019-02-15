using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Views
{
    
    // respresents login view with credentials.
    public interface ILoginView : IView
    {
        string Email { get; }
        string Password { get; }
    }
}
