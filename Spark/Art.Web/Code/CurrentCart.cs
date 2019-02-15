using Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web
{
    // stateful cart count is maintained by cookies

    public static class CurrentCart
    {
        public static int ItemCount
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies[".cartcount"];
                if (cookie == null) return 0;

                return int.Parse(cookie.Value);
            }
            set
            {
                var cookie = HttpContext.Current.Request.Cookies[".cartcount"];
                if (cookie == null) 
                {
                    cookie = new HttpCookie(".cartcount", value.ToString());
                    cookie.Expires = DateTime.Now.AddDays(30);
                }
                cookie.Value = value.ToString();

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}