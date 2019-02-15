using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web
{
    // a model of the user data carried by the custom principal

    public class CustomPrincipalModel
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}