using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Views
{
    // represents a single member view

    public interface IMemberView : IView
    {
        int MemberId { get; set; }
        string Email { get; set; }
        string CompanyName { get; set; }
        string City { get; set; }
        string Country { get; set; }
    }
}
