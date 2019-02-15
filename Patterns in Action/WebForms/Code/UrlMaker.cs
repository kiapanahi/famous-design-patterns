using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Code
{
    
    // helper to retrieve Urls

    public static class UrlMaker
    {
        // home page
        public static string ToDefault() { return Path("~/"); }

        // login / logout
        public static string ToLogin() { return Path("~/login"); }
        public static string ToLogout() { return Path("~/logout"); }

        // shopping 
        public static string ToShopping() { return Path("~/shop"); }
        public static string ToProducts() { return Path("~/shop/products"); }
        public static string ToProduct(int productId) { return Path("~/shop/products/{0}", productId); }
        public static string ToSearch() { return Path("~/shop/search"); }
        public static string ToCheckout() { return Path("~/shop/checkout"); }

        // administration
        public static string ToAdmin() { return Path("~/admin"); }
        public static string ToOrders() { return Path("~/admin/members/orders"); }
        public static string ToDetails(int memberId, int orderId) { return Path("~/admin/members/{0}/orders/{1}/details", memberId, orderId); }
        public static string ToMember(int memberId) { return Path("~/admin/members/{0}", memberId); }
        public static string ToMembers() { return Path("~/admin/members"); }

        // error page
        public static string ToError() { return Path("~/error"); }

        // private url builder helper
        static string Path(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        // overloaded private url builder helper
        static string Path(string virtualPath, params object[] args)
        {
            return Path(string.Format(virtualPath, args));
        }
    }
}