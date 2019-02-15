using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Models;

namespace WinFormsApp.Views
{
    /// respresents view of a list of members
    public interface IMembersView : IView
    {
        IList<MemberModel> Members { set; }
    }
}
