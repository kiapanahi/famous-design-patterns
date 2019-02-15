using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Threading;
using WebMatrix.WebData;
using System.Security.Principal;
using Art.Domain;

namespace Art.Web
{
    // wrapper which gets stateful user data from the current thread
    // ** small facade pattern

    public static class CurrentUser
    {
        // note: the first two properties are used on _Layout page

        public static bool IsAdmin { get { return Thread.CurrentPrincipal.IsInRole("Admin"); } }  
        public static bool IsAuthenticated { get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; } }

        public static int? Id { get { return (Thread.CurrentPrincipal is CustomPrincipal ? (Thread.CurrentPrincipal as CustomPrincipal).UserId : null); } }
        public static string FirstName { get { return (Thread.CurrentPrincipal is CustomPrincipal ? (Thread.CurrentPrincipal as CustomPrincipal).FirstName : ""); } }
        public static string LastName { get { return (Thread.CurrentPrincipal is CustomPrincipal ? (Thread.CurrentPrincipal as CustomPrincipal).LastName : ""); } }
        public static string FullName { get { return FirstName + " " + LastName; } }
    }
}