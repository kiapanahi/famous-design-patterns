using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Art.Web
{
    // a custom principal with authenticated user information

    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        string[] roles;

        public CustomPrincipal(string email, string[] roles)
        {
            this.Identity = new GenericIdentity(email);
            this.roles = roles;
        }

        public bool IsInRole(string role) { return roles.Contains(role); }

        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}