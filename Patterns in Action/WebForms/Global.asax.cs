using ActionService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebForms;
using WebForms.Code;
using WebForms.Code.Logging;
using WebMatrix.WebData;

namespace WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // initializes the SimpleMembership system and creates the 4 membership tables if they do not already exist
            // Action is the connectionstring name. Member is user table name. MemberId is user Identity. Email is used as UserName.

            WebSecurity.InitializeDatabaseConnection("Action", "Member", "MemberId", "Email", autoCreateTables: true);

            // create the two roles in the app if they to not already exist

            if (!Roles.RoleExists("Admin")) Roles.CreateRole("Admin");
            if (!Roles.RoleExists("Member")) Roles.CreateRole("Member");

            // get member and create potentially first admin

            var member = new Service().GetMemberByEmail("debbie@company.com");
            if (WebSecurity.GetCreateDate(member.Email) == DateTime.MinValue)
            {
                var email = "debbie@company.com";
                WebSecurity.CreateAccount(email, "secret123");
                Roles.AddUserToRole(email, "Admin");
            }

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();

            // initialize routing system

            RegisterRoutes(RouteTable.Routes);

            // initialize sitemap facility

            InitializeSiteMapResolver();

            // initialize logging facility

            InitializeLogger();
        }

        // register routes with routing system.

        private static void RegisterRoutes(RouteCollection routes)
        {
            // note: the order in which routes are registered is important.

            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("Error", "error", "~/Error.aspx");

            routes.MapPageRoute("Login", "login", "~/WebAuth/Login.aspx");
            routes.MapPageRoute("Logout", "logout", "~/WebAuth/Logout.aspx");

            routes.MapPageRoute("Shop", "shop", "~/WebShop/Shopping.aspx");
            routes.MapPageRoute("Products", "shop/products", "~/WebShop/Products.aspx");
            routes.MapPageRoute("Product", "shop/products/{productid}", "~/WebShop/Product.aspx");
            routes.MapPageRoute("Search", "shop/search", "~/WebShop/Search.aspx");
            routes.MapPageRoute("Cart", "shop/cart", "~/WebShop/Cart.aspx");
            routes.MapPageRoute("Checkout", "shop/checkout", "~/WebShop/Checkout.aspx");

            routes.MapPageRoute("Admin", "admin", "~/WebAdmin/Admin.aspx");
            routes.MapPageRoute("Orders", "admin/members/orders", "~/WebAdmin/Orders.aspx");
            routes.MapPageRoute("Order", "admin/members/{memberid}/orders", "~/WebAdmin/Order.aspx");
            routes.MapPageRoute("Details", "admin/members/{memberid}/orders/{orderid}/details", "~/WebAdmin/OrderDetails.aspx");
            routes.MapPageRoute("Member", "admin/members/{memberid}", "~/WebAdmin/Member.aspx");
            routes.MapPageRoute("Members", "admin/members", "~/WebAdmin/Members.aspx");

            routes.MapPageRoute("Default", "", "~/Default.aspx");
        }

        // initialize sitemap event facility

        private void InitializeSiteMapResolver()
        {
            SiteMap.SiteMapResolve += SiteMapResolveHandler;
        }

        // the Sitemap resolve event is handed over to the current page being processed 

        private SiteMapNode SiteMapResolveHandler(object sender, SiteMapResolveEventArgs e)
        {
            var pageBase = e.Context.CurrentHandler as PageBase;
            if (pageBase != null)
                return pageBase.SiteMapResolve(sender, e);
            else
                return null;
        }

        // initializes logging facility with severity level and observer(s).
        
        private void InitializeLogger()
        {
            // set system wide severity

            Logger.Instance.Severity = LogSeverity.Error; 

            // send log messages to debugger console (output window)
            // Note: the attach operation is the Observer pattern

            var logConsole = new ObserverLogToConsole();
            Logger.Instance.Attach(logConsole);

            // also send log messages to email (observer pattern)

            string from = "notification@yourcompany.com";
            string to = "webmaster@yourcompany.com";
            string subject = "Webmaster: please review";
            string body = "email text";
            var smtpClient = new SmtpClient("mail.yourcompany.com");
            
            var logEmail = new ObserverLogToEmail(from, to, subject, body, smtpClient);
            Logger.Instance.Attach(logEmail);

            // Other log output options (commented out)

            // send log messages to a file

            //var logFile = new ObserverLogToFile(@"C:\Temp\Error.log");
            //Logger.Instance.Attach(logFile);

            // send log message to event log

            //var logEvent = new ObserverLogToEventlog();
            //Logger.Instance.Attach(logEvent);

            // send log messages to database (observer pattern)

            //var logDatabase = new ObserverLogToDatabase();
            //Logger.Instance.Attach(logDatabase);
        }

        
        // this is the last-resort exception handler.
        // it uses te logging infrastructure to log the error details.
        // the application will then be redirected according to the 
        // customErrors configuration in web.config.
        
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError().GetBaseException();

            // NOTE: commented out because the site needs authorization to logging resources.
            // Logger.Instance.Error(ex.Message);

            // <customErrors ..> in web config will now redirect.
        }

    }
}
