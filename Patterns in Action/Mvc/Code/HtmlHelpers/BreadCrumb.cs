using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Code
{
    // represents a single crumb as part of breadcrumbs control

    public class BreadCrumb
    {
        public string Url { get; set; }
        public string Title { get; set; }
    }
}